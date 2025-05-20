using UnityEngine;

public class FireProjectile : Attack
{
    [Header("Projectile params: ")] 
    [SerializeField] private GameObject projectile;
    [SerializeField] private bool alternateProjectileFlip;
    private bool flip;
    private Vector3 flipScale = new(-1f, 1f, 1f);
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
        GameObject newProjectile = ObjectPoolManager.SpawnObject(projectile, transform.position, transform.rotation * projectile.transform.rotation, ObjectPoolManager.PoolType.GameObject);
        
        if (alternateProjectileFlip)
        {
            newProjectile.transform.localScale = flip ? flipScale : Vector3.one;
            flip = !flip;
        }
        
        if(SFX != null)
            SFXManager.Instance.PlaySFXClip(SFX, transform.position, SFXVolume, minPitch, maxPitch);
    }
}
