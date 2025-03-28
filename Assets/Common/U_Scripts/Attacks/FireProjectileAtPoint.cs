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
    [SerializeField] private AudioClip SFX;
    [SerializeField] private float SFXVolume = 1f;
    [SerializeField] private float minPitch = .9f, maxPitch = 1.1f;
    

    protected override void AttackTriggered()
    {
        base.AttackTriggered();
        
        FireProjectilePublic();
    }

    public void FireProjectilePublic()
    {
        ObjectPoolManager.SpawnObject(projectile, mouseAiming.CursorWorldPosition + objectPlacementOffset, projectile.transform.rotation, ObjectPoolManager.PoolType.GameObject);
        if(SFX != null)
            SFXManager.Instance.PlaySFXClip(SFX, transform.position, SFXVolume, minPitch, maxPitch);
    }
}
