using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class CharacterStatReadout : MonoBehaviour
{
    [SerializeField] private Stats characterStats;
    
    private void Start()
    {
        GetComponent<TMP_Text>().text = $"Move speed - {characterStats.moveSpeed}\nBonus max ammo - {characterStats.bonusMaxAmmo}\nFire rate modifier - {characterStats.fireDelayMultiplier*100}%\nReload time modifier - {characterStats.reloadTimeModifier*100}%\nAttack DMG modifier - {characterStats.attackDmgModifier*100}%";
    }
}
