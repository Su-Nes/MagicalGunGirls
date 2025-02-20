using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerDamageEnemy : MonoBehaviour
{
    [SerializeField] private float damage = 1;
    [SerializeField] private bool destroyOnHit = true;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out EnemyAI enemy))
            enemy.TakeDamage(damage);
        
        if(destroyOnHit)
            Destroy(gameObject);
    }
}
