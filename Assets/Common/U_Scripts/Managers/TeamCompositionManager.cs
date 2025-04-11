using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamCompositionManager : MonoBehaviour
{
    [SerializeField] private Transform viewport;
    private DataPersistenceManager dataPersistenceManager;

    private void Start()
    {
        Time.timeScale = 0f;
        
        dataPersistenceManager = FindObjectOfType<DataPersistenceManager>();
        
        foreach (GameObject character in dataPersistenceManager.UnlockedCharacters)
        {
            Instantiate(character.GetComponent<CharacterStatManager>().descriptionUIObj, Vector3.zero, Quaternion.identity, viewport);
        }
    }

    public void CloseTeamScreen()
    {
        if (FindObjectOfType<CharacterSwap>().CharacterCount > 0)
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
    }
}
