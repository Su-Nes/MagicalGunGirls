using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Progression/CharacterStats")]
public class Stats : ScriptableObject
{
    public float cooldownModifier = 1.5f;
    public float moveSpeed = 225f;
    public int maxAmmo = 3;
    public float fireDelay = 1f;
    public float reloadTime = 2f;
    public float attackDmgModifier;

    public void ApplyUpgrade(Upgrade upgrade)
    {
        FindObjectOfType<PlayerStatsManager>().ModifyMaxHealthValue(upgrade.bonusMaxHealth);
        
        cooldownModifier *= upgrade.cooldownModifierReductionMultiplication;
        reloadTime *= upgrade.reloadSpeedReductionMultiplication;
        fireDelay *= upgrade.fireDelayReductionMultiplication;
        
        attackDmgModifier += upgrade.bonusAttackModifier;
        
        moveSpeed += upgrade.bonusMoveSpeed;
        maxAmmo += upgrade.bonusAmmo;
    }
}
