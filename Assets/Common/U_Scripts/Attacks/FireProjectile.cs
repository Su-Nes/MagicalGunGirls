using UnityEngine;

public class FireProjectile : Attack
{
    [Header("Projectile params: ")]
    [SerializeField] private GameObject projectile;
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
        ObjectPoolManager.SpawnObject(projectile, transform.position, transform.rotation * projectile.transform.rotation, ObjectPoolManager.PoolType.GameObject);
        if(SFX != null)
            SFXManager.Instance.PlaySFXClip(SFX, transform.position, SFXVolume, minPitch, maxPitch);
    }
}
