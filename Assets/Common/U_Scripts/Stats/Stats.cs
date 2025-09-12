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
        cooldownModifier = Mathf.Clamp(cooldownModifier, 0, cooldownModifier);

        reloadTimeModifier += upgrade.ReloadSpeedMultReduction;
        reloadTimeModifier = Mathf.Clamp(reloadTimeModifier, 0, reloadTimeModifier);
        
        fireDelayMultiplier += upgrade.FireDelayMultReduction;
        fireDelayMultiplier = Mathf.Clamp(fireDelayMultiplier, 0, fireDelayMultiplier);

        attackDmgModifier += upgrade.BonusAttackModifier;
        
        moveSpeed += upgrade.BonusMoveSpeed;
        bonusMaxAmmo += upgrade.BonusAmmo;

        DataPersistenceManager.Instance.mendingNectar -= upgrade.Cost;
    }
}
