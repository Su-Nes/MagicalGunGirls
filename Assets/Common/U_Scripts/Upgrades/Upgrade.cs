using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Progression/Upgrade")]
public class Upgrade : ScriptableObject
{
    public string upgradeDescription = "Upgrade";
    
    
    public float bonusMaxHealth = 20f;
    
    public float cooldownModifierReductionMultiplication = .85f;
    public float reloadSpeedReductionMultiplication = .85f;
    public float fireDelayReductionMultiplication = .9f;
    
    public float bonusMoveSpeed = 10f;
    public int bonusAmmo = 1;
    public float bonusAttackModifier = .15f;
}
