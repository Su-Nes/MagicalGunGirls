using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCharacterStats : MonoBehaviour
{
    [SerializeField] private KeyCode resetKey = KeyCode.Backslash;
    [SerializeField] private Stats[] defaultStatObjects, targetStatObjects;

    private void Update()
    {
        if (Input.GetKeyDown(resetKey))
            Reset();
    }

    private void Reset()
    {
        int i = 0;
        foreach (Stats stat in defaultStatObjects)
        {
            targetStatObjects[i].cooldownModifier = stat.cooldownModifier;
            targetStatObjects[i].moveSpeed = stat.moveSpeed;
            targetStatObjects[i].bonusMaxAmmo = stat.bonusMaxAmmo;
            targetStatObjects[i].fireDelayMultiplier = stat.fireDelayMultiplier;
            targetStatObjects[i].reloadTimeModifier = stat.reloadTimeModifier;
            targetStatObjects[i].attackDmgModifier = stat.attackDmgModifier;
            i++;
        }
        
        FindObjectOfType<CharacterSwap>().ResetCharacter();
    }
}
