using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddCharacterToTeam : MonoBehaviour
{
    [SerializeField] private GameObject characterPrefab;
    private CharacterSwap characterSwap;
    
    private void Start()
    {
        characterSwap = FindObjectOfType<CharacterSwap>();
    }

    public void AddCharacter()
    {
        characterSwap.AddCharacter(characterPrefab);
    }
}
