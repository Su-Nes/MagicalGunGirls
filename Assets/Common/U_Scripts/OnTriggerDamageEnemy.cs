using System;
using System.Collections;
using UnityEngine;

public class OnTriggerDamageEnemy : MonoBehaviour
{
    [SerializeField] private Stats characterStatSO;
    [SerializeField] private float damage = 1, lifetime = 1f, delayBetweenHits = .5f;
    private float timer = 999999f;
    [SerializeField] private bool monitoring = true, destroyOnHit = true, continuousDamage, destroysProjectiles;
    private bool startState;
    [SerializeField] private GameObject overwriteDisableObj;

    private void Awake()
    {
        startState = monitoring;
        if (overwriteDisableObj == null)
            overwriteDisableObj = gameObject;
    }

    private void OnEnable() // all of this is due to object pooling!
    {
        monitoring = startState;
        if(lifetime != 0f)
            StartCoroutine(DisableAfterTime(lifetime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!monitoring || continuousDamage)
            return;
        
        if(other.TryGetComponent(out EnemyAI enemy))
            enemy.TakeDamage(damage * characterStatSO.attackDmgModifier);

        if (destroysProjectiles)
        {
            if (other.TryGetComponent(out MoveObjectContinuous p))
            {
                ObjectPoolManager.ReturnObjectToPool(p.gameObject);
            }
        }

        if (other.gameObject.layer == 6) // don't destroy on collision with ground
            return;

        if (destroyOnHit)
        {
            
            ObjectPoolManager.ReturnObjectToPool(overwriteDisableObj);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!monitoring || !continuousDamage)
            return;

        timer += Time.deltaTime;
        if (timer >= delayBetweenHits)
        {
            if(other.TryGetComponent(out EnemyAI enemy))
                enemy.TakeDamage(damage * characterStatSO.attackDmgModifier);
        
            if(destroyOnHit)
                ObjectPoolManager.ReturnObjectToPool(overwriteDisableObj);

            timer = 0f;
        }
    }

    private IEnumerator DisableAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPoolManager.ReturnObjectToPool(overwriteDisableObj);
    }

    public void SetMonitoring(bool state)
    {
        monitoring = state;
    }
}
