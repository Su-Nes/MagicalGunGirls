using System;
using System.Collections.Generic;
using UnityEngine;

public class RepelAbility : Ability
{
    [Header("Repel params: ")] 
    [SerializeField] private GameObject effectObject;
    [SerializeField] private float effectLifetime;
    [SerializeField] private float radius = 4f;
    [SerializeField] private float force = 6f;
    [SerializeField] private float stunTime = 1.5f;
    [SerializeField] protected float damage;
    

    protected override void ActivateAbility()
    {
        base.ActivateAbility();
        
        GameObject newEffect = Instantiate(effectObject, transform.position, transform.rotation);
        Destroy(newEffect, effectLifetime);
        
        
        foreach (Collider col in Physics.OverlapSphere(transform.position, radius))
        {
            if (col.TryGetComponent(out EnemyAI enemy))
            {
                enemy.InflictStun(stunTime);
                enemy.TakeDamage(damage);
            }
            
            /*if (col.TryGetComponent(out PlayerMovement playerMovement))
                    return; // don't affect player rigidbody*/
            
            if(col.TryGetComponent(out Rigidbody rb))
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }

            if (col.TryGetComponent(out MoveObjectContinuous moveObjectContinuous))
            {
                moveObjectContinuous.SetMovementDirection(col.transform.position - transform.position);
                
            }
            
            if (col.TryGetComponent(out OnTriggerDamageEnemy onTriggerDamageEnemy))
                onTriggerDamageEnemy.SetMonitoring(true);
        }
    }
}
