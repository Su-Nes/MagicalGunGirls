using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Progression/CharacterStats")]
public class Stats : ScriptableObject
{
    public float cooldownModifier = 1.5f;
    public float moveSpeed = 225f;
    public int bonusMaxAmmo;
    public float fireDelayMultiplier = 1f;
    public float reloadTimeModifier = 1f;
    public float attackDmgModifier = 1f;

    public void ApplyUpgrade(Upgrade upgrade)
    {
        FindObjectOfType<PlayerStatsManager>().ModifyMaxHealthValue(upgrade.bonusMaxHealth);
        
        cooldownModifier += upgrade.cooldownModifierMultReduction;
        reloadTimeModifier += upgrade.reloadSpeedMultReduction;
        fireDelayMultiplier += upgrade.fireDelayMultReduction;
        attackDmgModifier += upgrade.bonusAttackModifier;
        
        moveSpeed += upgrade.bonusMoveSpeed;
        bonusMaxAmmo += upgrade.bonusAmmo;
    }
}
