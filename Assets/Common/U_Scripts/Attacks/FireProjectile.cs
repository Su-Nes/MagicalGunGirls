using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : Attack
{
    [Header("Projectile params: ")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileLifetime = 1f;
    

    protected override void PrimaryAttackTriggered()
    {
        base.PrimaryAttackTriggered();
        
        GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
        newProjectile.transform.SetParent(null);
        Destroy(newProjectile, projectileLifetime);
    }

    public void FireProjectilePublic()
    {
        PrimaryAttackTriggered();
    }
}
