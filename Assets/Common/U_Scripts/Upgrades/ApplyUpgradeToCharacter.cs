using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ApplyUpgradeToCharacter : MonoBehaviour
{
    [SerializeField] private Stats characterStats;
    [SerializeField] private TMP_Text statsText;
    public Upgrade upgradeToApply;
    private CharacterSwap characterSwap;
    private BarracksUpgradeManager barracks;
    
    
    private void Awake()
    {
        characterSwap = FindObjectOfType<CharacterSwap>();
        barracks = FindObjectOfType<BarracksUpgradeManager>();

        statsText.text = $"Move speed - {characterStats.moveSpeed}\nBonus max ammo - {characterStats.bonusMaxAmmo}\nFire rate modifier - {characterStats.fireDelayMultiplier*100}%\nReload time modifier - {characterStats.reloadTimeModifier*100}%\nAttack DMG modifier - {characterStats.attackDmgModifier*100}%";
    }
    
    public void ApplyUpgrade()
    {
        characterStats.ApplyUpgrade(upgradeToApply);
        barracks.FinishUpgrade();
    }
}
