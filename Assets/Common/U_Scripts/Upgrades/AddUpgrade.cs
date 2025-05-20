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

/*[CustomEditor(typeof(AddUpgrade))]
class DebugUpgrades : Editor {
    
    
    public override void OnInspectorGUI() {
        if (GUILayout.Button("Test Upgrade"))
        {
            debugStats.ApplyUpgrade(debugUpgrade);
        }
    }
}*/
