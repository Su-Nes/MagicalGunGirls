using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class AddUpgrade : MonoBehaviour
{
    [SerializeField] private Stats debugStats;
    [SerializeField] private Upgrade debugUpgrade;

    private void Start()
    {
        ApplyUpgrade(debugStats, debugUpgrade);
    }

    public void ApplyUpgrade(Stats character, Upgrade upgrade)
    {
        character.ApplyUpgrade(upgrade);
    }
}
