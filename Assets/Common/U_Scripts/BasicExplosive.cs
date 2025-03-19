using System.Collections;
using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public class BasicExplosive : MonoBehaviour
{
    [SerializeField] private GameObject effectObject;
    [SerializeField] private float effectLifetime = 1f;
    
    [Header("Physics params: ")]
    [SerializeField] private float throwForce;
    [SerializeField] private bool explodeOnImpact;
    [SerializeField] private float impactDetectionDelay = .5f;
    [SerializeField] private float explodeTimer;
    [SerializeField] private float objectLifetime = 2f;

    [Header("Damage params: ")] 
    [SerializeField] private float radius = 10f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float stunTime = .5f;
    [SerializeField] private float knockBack = 10f;

    
    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
        
        if(!explodeOnImpact)
            StartCoroutine(ExplodeAfterTime(explodeTimer));
        
        Destroy(gameObject, objectLifetime);
    }

    private void Update()
    {
        if(explodeOnImpact)
            impactDetectionDelay -= Time.deltaTime;
    }

    private IEnumerator ExplodeAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Explode();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (explodeOnImpact && impactDetectionDelay <= 0f)
        {
            Explode();
        
            //print(other.gameObject.name);
        }
    }

    private void Explode()
    {
        GetComponent<Renderer>().enabled = false;
        
        GameObject newEffect = Instantiate(effectObject, transform.position, transform.rotation);
        Destroy(newEffect, effectLifetime);
        
        foreach (Collider col in Physics.OverlapSphere(transform.position, radius))
        {
            if (col == GetComponent<Collider>())
                return;
            
            if (col.TryGetComponent(out EnemyAI enemy))
            {
                enemy.TakeDamage(damage);
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
