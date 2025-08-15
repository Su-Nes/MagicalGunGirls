using UnityEngine;

public class FireProjectile : Attack
{
    [Header("Projectile params: ")] 
    [SerializeField] private GameObject projectile;
    [SerializeField] private bool alternateProjectileFlip;
    private bool flip;
    private Vector3 flipScale = new(-1f, 1f, 1f);
    
    
    protected override void AttackTriggered()
    {
        FireProjectilePublic();
    }

    public void FireProjectilePublic()
    {
        base.AttackTriggered();
        
        GameObject newProjectile = ObjectPoolManager.SpawnObject(projectile, transform.position, transform.rotation * projectile.transform.rotation, ObjectPoolManager.PoolType.GameObject);
        
        if (alternateProjectileFlip)
        {
            newProjectile.transform.localScale = flip ? flipScale : Vector3.one;
            flip = !flip;
        }
    }
}
