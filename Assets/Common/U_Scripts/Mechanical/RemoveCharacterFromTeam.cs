using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveCharacterFromTeam : MonoBehaviour
{
    [SerializeField] private GameObject characterPrefab;
    private CharacterSwap characterSwap;
    
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(RemoveCharacter);
        characterSwap = FindObjectOfType<CharacterSwap>();
    }

    private void RemoveCharacter()
    {
        characterSwap.RemoveCharacter(characterPrefab);
    }
}
