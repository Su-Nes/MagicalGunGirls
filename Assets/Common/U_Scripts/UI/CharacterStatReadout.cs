using System;
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
        GetComponent<TMP_Text>().text = $"Attack DMG modifier - {Mathf.RoundToInt(characterStats.attackDmgModifier*100)}%\n" +
                                        $"Reload time modifier - {Mathf.RoundToInt(characterStats.reloadTimeModifier*100)}%\n" +
                                        $"Cooldown modifier - {Mathf.RoundToInt(characterStats.cooldownModifier*100)}%\n" +
                                        $"Shot delay modifier - {Mathf.RoundToInt(characterStats.fireDelayMultiplier * 100)}%\n" +
                                        $"Bonus max ammo - {characterStats.bonusMaxAmmo}\n" +
                                        $"Move speed - {characterStats.moveSpeed}\n";
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
            Start();
    }
}
