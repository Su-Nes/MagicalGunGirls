using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Progression/Upgrade")]
public class Upgrade : ScriptableObject
{
    public string upgradeDescription = "Upgrade";
    public int cost;
    
    public float bonusMaxHealth = 20f;
    
    public float cooldownModifierMultReduction = -.15f;
    public float reloadSpeedMultReduction = -.15f;
    public float fireDelayMultReduction = -.15f;
    public float bonusAttackModifier = .15f;
    
    public float bonusMoveSpeed = 10f;
    public int bonusAmmo = 1;
}
