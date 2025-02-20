using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent))]
[RequireComponent(typeof (Rigidbody))]
public class EnemyAI : MonoBehaviour
{
    protected NavMeshAgent _navMeshAgent;
    //protected CharacterController _characterController;
    protected Rigidbody _rigidbody;
    protected Transform target;
    [SerializeField] protected SpriteFlicker _spriteFlicker;
    
    [SerializeField] private float maxHealth = 5;
    private float health;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float fallVelocity = 5f;
    [SerializeField] private float stopDistance = .1f;
    private float stunTime;
    
        
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        //_characterController = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        
        _navMeshAgent.updatePosition = false;
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.speed = moveSpeed / 100f;

        health = maxHealth;
    }

    private void Update()
    {
        _navMeshAgent.SetDestination(target.position);
        Vector3 dir = _navMeshAgent.nextPosition - transform.position;
        Vector3 dirFlat = new(dir.x, -fallVelocity, dir.z);
        if(Vector3.Distance(transform.position, _navMeshAgent.nextPosition) > stopDistance) // buffer to make sure the movement doesn't go weird when it's too close to the destination
            Move(dirFlat.normalized, moveSpeed * Time.deltaTime);
        
        if (health <= 0f)
            Die();
    }

    protected virtual void Move(Vector3 dir, float speed)
    {
        //_characterController.Move(Vector3.down * fallVelocity);
        //_characterController.Move(dir * (speed * Time.deltaTime));
        if (stunTime <= 0f)
            _rigidbody.velocity = dir * speed;
        else
            stunTime -= Time.deltaTime;
    }

    public void TakeDamage(float healthValue)
    {
        health -= healthValue;
        _spriteFlicker.Flicker();
    }

    public void InflictStun(float stunLength)
    {
        stunTime = stunLength;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
