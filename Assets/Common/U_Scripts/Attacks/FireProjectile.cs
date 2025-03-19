using UnityEngine;

public class FireProjectile : Attack
{
    [Header("Projectile params: ")]
    [SerializeField] private GameObject projectile;
    

    protected override void AttackTriggered()
    {
        base.AttackTriggered();
        
        FireProjectilePublic();
    }

    public void FireProjectilePublic()
    {
        ObjectPoolManager.SpawnObject(projectile, transform.position, transform.rotation, ObjectPoolManager.PoolType.GameObject);
    }
}
