using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public class BasicExplosive : MonoBehaviour
{
    private enum StartDirection
    {
        Forward,
        Backward,
        Left,
        Right,
        Up,
        Down
    }
    
    [Header("INSERT STATS OBJECT")]
    [SerializeField] private Stats characterStatSO;
    [Header("END STATS OBJECT")]
    
    [SerializeField] private Renderer modelRenderer;
    [SerializeField] private GameObject effectObject;
    [SerializeField] private float effectLifetime = 1f;
    
    [Header("Physics params: ")]
    [SerializeField] private float throwForce;
    [SerializeField] private StartDirection throwDirection;
    private Vector3 direction;
    [SerializeField] private bool explodeOnImpact;
    [SerializeField] private float impactDetectionDelay = .5f;
    private float impactDetectionTimer;
    [SerializeField] private float explodeTimer;
    [SerializeField] private float objectLifetime = 2f;

    [Header("Damage params: ")] 
    [SerializeField] private float radius = 10f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float stunTime = .5f;
    [SerializeField] private float knockBack = 10f;
    
    
    private void Start()
    {
        direction = throwDirection switch
        {
            StartDirection.Forward => transform.forward,
            StartDirection.Backward => -transform.forward,
            StartDirection.Left => -transform.right,
            StartDirection.Right => transform.right,
            StartDirection.Up => transform.up,
            StartDirection.Down => -transform.up,
            _ => transform.forward
        };
        
        GetComponent<Rigidbody>().AddForce(direction * throwForce, ForceMode.Impulse);
        
        StartCoroutine(ExplodeAfterTime(explodeTimer));
        
        Destroy(gameObject, objectLifetime);
    }

    private void OnEnable()
    {
        impactDetectionTimer = impactDetectionDelay;
        modelRenderer.enabled = true;
    }

    private void Update()
    {
        impactDetectionTimer -= Time.deltaTime;
    }

    private IEnumerator ExplodeAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Explode();
    }

    private void OnCollisionEnter(Collision other)
    {
        print(other.gameObject.name);
        if (explodeOnImpact && impactDetectionTimer <= 0f)
        {
            Explode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (explodeOnImpact && impactDetectionTimer <= 0f)
        {
            Explode();
        }
    }

    private void Explode()
    {
        if (!modelRenderer.enabled)
            return;
        
        modelRenderer.enabled = false; // use this as the check for whether the explosion has gone off yet
        
        GameObject newEffect = Instantiate(effectObject, transform.position, transform.rotation);
        Destroy(newEffect, effectLifetime);
        
        foreach (Collider col in Physics.OverlapSphere(transform.position, radius))
        {
            if (col == GetComponent<Collider>())
                return;
            
            if (col.TryGetComponent(out EnemyAI enemy))
            {
                enemy.TakeDamage(damage * characterStatSO.attackDmgModifier);
                enemy.InflictStun(stunTime);
            }
            
            if(col.TryGetComponent(out Rigidbody rb))
            {
                float distanceFromOrigin = Vector3.Distance(transform.position, rb.transform.position); // further away from player when repel = less force applied
                rb.AddForce((rb.transform.position - transform.position).normalized * knockBack / distanceFromOrigin, ForceMode.Impulse);
            }
        }
    }
}
