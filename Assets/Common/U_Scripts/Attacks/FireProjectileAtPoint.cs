using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectileAtPoint : Attack
{
    [Header("Projectile params: ")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private MouseAiming mouseAiming;
    [SerializeField] private Vector3 objectPlacementOffset;
    

    protected override void AttackTriggered()
    {
        base.AttackTriggered();
        
        FireProjectilePublic();
    }

    public void FireProjectilePublic()
    {
        ObjectPoolManager.SpawnObject(projectile, mouseAiming.CursorWorldPosition + objectPlacementOffset, projectile.transform.rotation, ObjectPoolManager.PoolType.GameObject);
    }
}
