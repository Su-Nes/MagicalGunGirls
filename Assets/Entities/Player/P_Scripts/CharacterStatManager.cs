using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterStatManager : MonoBehaviour
{
    [SerializeField] private string characterName = "MagGirl";
    [SerializeField] private GameObject nameTextUIObj, ammoUIObj, cooldownUIObj, abilityUIObj;
    
    [Header("The Managers.")]
    [SerializeField] private AmmoManager[] _ammoManagers;
    [SerializeField] private CooldownManager[] _cooldownManagers;
    [SerializeField] private CooldownManager[] _abilityCooldownManagers;
    
    private Transform characterUI;
    
    
    private void Awake() // instantiate all weapon and ability UI objects and assign their managers
    {
        characterUI = GameObject.Find("CharacterSelectLayout").transform.GetChild(transform.GetSiblingIndex());

        GameObject nameObj = Instantiate(nameTextUIObj, Vector3.zero, Quaternion.identity, characterUI);
        nameObj.GetComponent<TMP_Text>().text = characterName;
        
        foreach (AmmoManager a_m in _ammoManagers)
        {
            GameObject newManager = Instantiate(ammoUIObj, Vector3.zero, Quaternion.identity, characterUI);
            newManager.GetComponent<ReadAmmoCount>()._AmmoManager = a_m; // read script that takes ammo value from assigned manager
        }
        
        foreach (CooldownManager c_m in _cooldownManagers)
        {
            GameObject newManager = Instantiate(cooldownUIObj, Vector3.zero, Quaternion.identity, characterUI);
            newManager.GetComponent<ReadChargeAmount>()._CooldownManager = c_m;
        }
        
        foreach (CooldownManager c_m in _abilityCooldownManagers)
        {
            GameObject newManager = Instantiate(abilityUIObj, Vector3.zero, Quaternion.identity, characterUI);
            newManager.GetComponent<ReadChargeAmount>()._CooldownManager = c_m;
        }
    }
}
