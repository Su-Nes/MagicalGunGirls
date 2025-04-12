using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamCompositionManager : MonoBehaviour
{
    [SerializeField] private Transform viewport;

    private void Start()
    {
        Time.timeScale = 0f;
        
        foreach (GameObject character in DataPersistenceManager.Instance.UnlockedCharacters)
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
