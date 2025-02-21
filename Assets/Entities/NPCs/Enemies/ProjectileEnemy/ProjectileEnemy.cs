using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : EnemyAI
{
    [Header("Projectile params: ")]
    [SerializeField] protected float firePeriod;
    private float fireTimer;
    [SerializeField] protected FireProjectile _fireProjectile;
    [SerializeField] protected float lookAheadMod;
    

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (fireTimer < firePeriod)
            fireTimer += Time.deltaTime;
        else
        {
            Vector3 aimTarget = new Vector3(target.position.x, _fireProjectile.transform.position.y, target.position.z) +
                                target.GetComponent<PlayerMovement>().GetVelocity().normalized * lookAheadMod;
            _fireProjectile.transform.LookAt(aimTarget);
                
            _fireProjectile.FireProjectilePublic();
            fireTimer = 0f;
        }
    }
}
