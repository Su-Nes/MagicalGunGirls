using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof (Rigidbody))]
public class EnemyAI : MonoBehaviour
{
    protected NavMeshPath path;
    private int cornerIndex;
    private CharacterController _characterController;
    private Rigidbody _rigidBody;
    protected Transform target;
    
    [SerializeField] private float maxHealth = 5;
    protected float health;
    [SerializeField] protected float moveSpeed;
    [SerializeField] private float fallVelocity = 5f;
    private float stunTime;
    
    [SerializeField] protected SpriteFlicker _spriteFlicker;
    
    
        
    protected virtual void Awake()
    {
        path = new NavMeshPath();

        _characterController = GetComponent<CharacterController>();
        _rigidBody = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();

        health = maxHealth;
    }

    protected virtual void FixedUpdate()
    {
        if (GameManager.Instance.FreezePlayer)
            return;
        
        if (_characterController != null && !_characterController.enabled)
            return;
        
        if (NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path))
        {
            Vector3 dir = path.corners[1] - transform.position;
            Vector3 dirFlat = new(dir.x, 0f, dir.z);
            
            Move(dirFlat.normalized, moveSpeed * Time.deltaTime);
        }

        // dies if not found on nav mesh
        if (!NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 5f, NavMesh.AllAreas) && moveSpeed > 0)
        {
            print($"{name} deleted because entity is off nav mesh");
            Die();
        }
        
        if (health <= 0f)
            Die();
    }

    protected virtual void Move(Vector3 dir, float speed)
    {
        if (_characterController.enabled)
        {
            _characterController.Move(Vector3.down * fallVelocity);
            _characterController.Move(dir * (speed * Time.deltaTime));
        }
    }

    public virtual void TakeDamage(float healthValue)
    {
        health -= healthValue;
        _spriteFlicker.Flicker();
    }

    public void InflictStun(float stunLength)
    {
        if (_characterController != null)
            StartCoroutine(StunTime(stunLength));
    }

    private IEnumerator StunTime(float time)
    {
        _rigidBody.isKinematic = false;
        _characterController.enabled = false;
        GetComponent<Collider>().isTrigger = false;
        
        yield return new WaitForSeconds(time);
        
        _rigidBody.isKinematic = true;
        _characterController.enabled = true;
        GetComponent<Collider>().isTrigger = true;
    }

    protected virtual void Die()
    {
        health = maxHealth; // for when this gets re-enabled in the pool
        
        if (ObjectPoolManager.IsInPool(gameObject))
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        else 
            Destroy(gameObject);    
        
        if (ScoreManager.Instance is not null)
            ScoreManager.Instance.UpdateScore(1);
    }
}
