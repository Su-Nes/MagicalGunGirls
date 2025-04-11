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
        GetComponent<Button>().onClick.AddListener(AddCharacter);
        characterSwap = FindObjectOfType<CharacterSwap>();
    }

    private void AddCharacter()
    {
        characterSwap.AddCharacter(characterPrefab);
    }
}
