using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [TextArea(1, 3)]
    public string UpgradeDescription;
    public int Cost;
    
    public float BonusMaxHealth;

    public float CooldownModifierMultReduction;
    public float ReloadSpeedMultReduction;
    public float FireDelayMultReduction;
    public float BonusAttackModifier;
    
    public float BonusMoveSpeed;
    public int BonusAmmo;
    
    
}
