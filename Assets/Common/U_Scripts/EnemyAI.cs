using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof (Rigidbody))]
[RequireComponent(typeof(PlayRandomSound))]
public class EnemyAI : MonoBehaviour
{
    protected NavMeshPath path;
    private int cornerIndex;
    private CharacterController _characterController;
    private Rigidbody _rigidBody;
    protected Transform target;
    protected Vector3 targetPos;

    [SerializeField] protected float maxHealth = 5;
    [SerializeField] protected float health;
    [SerializeField] protected float moveSpeed;
    [SerializeField] private float fallVelocity = 5f;
    private float stunTime, spawnInvincibilityTimer, spawnInvincibilityTime = .5f;
    
    [SerializeField] protected SpriteFlicker _spriteFlicker;
    [SerializeField] protected GameObject deathParticles, nectarOrb;
    [SerializeField] protected float nectarOrbDropChance;
    [SerializeField] protected AudioClip hurtSound;
    
        
    protected virtual void Awake()
    {
        path = new NavMeshPath();

        _characterController = GetComponent<CharacterController>();
        _rigidBody = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();

        health = maxHealth;
        spawnInvincibilityTimer = spawnInvincibilityTime;
    }

    protected virtual void FixedUpdate()
    {
        spawnInvincibilityTimer -= Time.deltaTime;
        
        if (GameManager.Instance.FreezeEnemies)
            return;
        
        if (_characterController != null && !_characterController.enabled)
            return;

        targetPos = target.position;
        targetPos.y = transform.position.y;
        
        if (NavMesh.CalculatePath(transform.position, targetPos, NavMesh.AllAreas, path))
        {
            Vector3 dir = path.corners[1] - transform.position;
            Vector3 dirFlat = new(dir.x, 0f, dir.z);
            
            Move(dirFlat.normalized, moveSpeed * Time.deltaTime);
        }

        // dies if not found on nav mesh
        if (!NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 5f, NavMesh.AllAreas) && moveSpeed > 0)
        {
            Debug.LogWarning($"{name} deleted because entity is off nav mesh");
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
        if (spawnInvincibilityTimer > 0f)
            return;

        health -= healthValue;
        _spriteFlicker.Flicker();
        SFXManager.Instance.PlaySFXClip(hurtSound, transform.position, .5f, .7f, 1.05f);
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
        spawnInvincibilityTimer = spawnInvincibilityTime;

        // Notify MissionManager BEFORE destroying or pooling
        if (MissionManager.Instance != null)
            MissionManager.Instance.EnemyKilled();

        ObjectPoolManager.SpawnObject(deathParticles, transform.position, deathParticles.transform.rotation,
            ObjectPoolManager.PoolType.GameObject);

        if (Random.Range(0f, 1f) < nectarOrbDropChance) // randomly drop mending nectar orb
            ObjectPoolManager.SpawnObject(nectarOrb, transform.position, Quaternion.identity, ObjectPoolManager.PoolType.GameObject);
        
        GetComponent<PlayRandomSound>().Play(); // death SFX
        
        if (ScoreManager.Instance is not null)
            ScoreManager.Instance.UpdateScore(1);
        
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}
