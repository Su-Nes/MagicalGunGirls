using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionDamageEnemy : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f, damageInterval = 1f, lifetime = 5f;
    [SerializeField] private bool destroyOnHit;
    private EnemyAI enemy;
    private float damageTimer;


    private void Start()
    {
        if(lifetime != 0f)
            StartCoroutine(DisableAfterTime(lifetime));
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out EnemyAI e))
        {
            enemy = e;
        }else if (destroyOnHit && !other.gameObject.CompareTag("Enemy"))
            Destroy(gameObject);
    }

    private void Update()
    {
        if (enemy != null)
        {
            if (damageTimer <= 0f)
            {
                damageTimer = damageInterval;
                enemy.TakeDamage(damageAmount);
                
                if(destroyOnHit)
                    Destroy(gameObject);
            }

            damageTimer -= Time.deltaTime;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.TryGetComponent(out PlayerStatsManager p))
        {
            damageTimer = 0f;
            enemy = null;
        }
    }
    
    private IEnumerator DisableAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}
