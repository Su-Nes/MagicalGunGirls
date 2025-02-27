using System.Collections;
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
            
            if(col.TryGetComponent(out Rigidbody rb))
            {
                float distanceFromOrigin = Vector3.Distance(transform.position, rb.transform.position); // further away from player when repel = less force applied
                //rb.AddForce((rb.transform.position - transform.position).normalized * force / distanceFromOrigin, ForceMode.Impulse);
                rb.AddExplosionForce(force, transform.position, radius);
            }
            
            if (col.TryGetComponent(out MoveObjectContinuous moveObjectContinuous))
                moveObjectContinuous.ReverseMoveDirection();
            
            if (col.TryGetComponent(out OnTriggerDamageEnemy onTriggerDamageEnemy))
                onTriggerDamageEnemy.SetMonitoring(true);
        }
    }
}
