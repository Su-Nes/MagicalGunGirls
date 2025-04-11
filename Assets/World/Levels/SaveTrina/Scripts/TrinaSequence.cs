using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class TrinaSequence : MonoBehaviour
{
    [SerializeField] private GameObject trinaObj, trinaPlayableObject;
    [SerializeField] private DebugEnemySpawner enemySpawner;
    [SerializeField] private Transform enemyParent;
    private bool sequenceFinished;
    
    [YarnCommand("TrinaEnemyEncounter")]
    public void TrinaEnemyEncounter()
    {
        trinaObj.SetActive(false);
        enemySpawner.SpawnGroup();
        
        GameManager.Instance.GetComponentInChildren<DataPersistenceManager>().UnlockCharacter(trinaPlayableObject);
        FindObjectOfType<CharacterSwap>().AddCharacter(trinaPlayableObject, true);
    }

    private void Update()
    {
        if (enemyParent.childCount <= 0 && !sequenceFinished)
        {
            sequenceFinished = true;
            GameManager.Instance.LoadSceneWithName("BaseCamp_Evening");
        }
    }
}
