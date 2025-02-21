using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof (NavMeshAgent))]
[RequireComponent(typeof (Rigidbody))]
public class EnemyAI : MonoBehaviour
{
    protected NavMeshPath path;
    private int cornerIndex;
    protected CharacterController _characterController;
    protected Rigidbody _rigidBody;
    protected Transform target;
    [SerializeField] protected SpriteFlicker _spriteFlicker;
    
    [SerializeField] private float maxHealth = 5;
    private float health;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float fallVelocity = 5f;
    private float stunTime;
    
        
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
        if (!_characterController.enabled)
            return;
        
        if (NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path))
        {
            /*for (int i = 0; i < path.corners.Length - 1; i++)
                Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);*/
            
            Vector3 dir = path.corners[1] - transform.position;
            Vector3 dirFlat = new(dir.x, 0f, dir.z);
            
            Move(dirFlat.normalized, moveSpeed * Time.deltaTime);
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
        //_rigidBody.velocity = dir * speed;
    }

    public void TakeDamage(float healthValue)
    {
        health -= healthValue;
        _spriteFlicker.Flicker();
    }

    public void InflictStun(float stunLength)
    {
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

    private void Die()
    {
        Destroy(gameObject);
    }
}
