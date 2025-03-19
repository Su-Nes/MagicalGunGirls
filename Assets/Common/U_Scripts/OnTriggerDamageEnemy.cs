using UnityEngine;

public class OnTriggerDamageEnemy : MonoBehaviour
{
    [SerializeField] private float damage = 1;
    [SerializeField] private bool monitoring = true;
    private bool startState;
    [SerializeField] private bool destroyOnHit = true;

    private void Awake()
    {
        startState = monitoring;
    }

    private void OnEnable() // all of this is due to object pooling!
    {
        monitoring = startState;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!monitoring)
            return;
        
        if(other.TryGetComponent(out EnemyAI enemy))
            enemy.TakeDamage(damage);
        
        if(destroyOnHit)
            ObjectPoolManager.ReturnObjectToPool(gameObject);
    }

    public void SetMonitoring(bool state)
    {
        monitoring = state;
    }
}
