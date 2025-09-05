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
        DataPersistenceManager.Instance.maxPlayerHealth += upgrade.BonusMaxHealth;
        
        cooldownModifier += upgrade.CooldownModifierMultReduction;
        reloadTimeModifier += upgrade.ReloadSpeedMultReduction;
        fireDelayMultiplier += upgrade.FireDelayMultReduction;
        attackDmgModifier += upgrade.BonusAttackModifier;
        
        moveSpeed += upgrade.BonusMoveSpeed;
        bonusMaxAmmo += upgrade.BonusAmmo;
    }
}
