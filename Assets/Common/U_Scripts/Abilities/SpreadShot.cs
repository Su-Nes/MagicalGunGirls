using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShot : Attack
{
    [Header("Spread shot params!")]
    [SerializeField] private FireProjectile[] projectiles;
    
    protected override void AttackTriggered()
    {
        base.AttackTriggered();

        foreach (FireProjectile proj in projectiles)
        {
            proj.FireProjectilePublic();
        }
    }
}
