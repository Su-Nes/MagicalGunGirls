using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof (Rigidbody))]
public class ConstantForceTowardsTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float baseForce = 1f;
    [SerializeField] private float pullRadius = 5f;
    private float force;
    [SerializeField] private float randomForce = 1f;
    
    private Rigidbody rb;
    
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (target == null)
            target = transform.parent;

        transform.position = target.position;
    }

    private void FixedUpdate()
    {
        force = baseForce * Vector3.Distance(target.position, transform.position) / pullRadius;
        
        Vector3 direction = (target.position - transform.position).normalized;
        rb.AddForce(direction * force + Random.insideUnitSphere * randomForce);
    }
}
