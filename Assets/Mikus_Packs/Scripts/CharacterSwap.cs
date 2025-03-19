using System.Collections.Generic;
using UnityEngine;

public class CharacterSwap : MonoBehaviour
{
    [SerializeField] private List<GameObject> characterPrefabs;
    [SerializeField] private GameObject panelUIObj;
    private Transform UIParent;
    private GameObject currentCharacter;
    private int whichCharacter;

    private void Awake()
    {
        UIParent = GameObject.Find("CharacterSelectLayout").transform;
        foreach (GameObject ch in characterPrefabs)
        {
            Instantiate(panelUIObj, Vector3.zero, Quaternion.identity, UIParent);
            Instantiate(ch, transform.position, Quaternion.identity, transform);
        }
    }

    private void Start()
    {
        if (characterPrefabs.Count > 0)
        {
            whichCharacter = 0;
            Swap();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            whichCharacter = whichCharacter == 0 ? transform.childCount - 1 : whichCharacter - 1;
            Swap();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            whichCharacter = whichCharacter == transform.childCount - 1 ? 0 : whichCharacter + 1;
            Swap();
        }
    }

    private void Swap()
    {
        foreach (SetActiveCharacter ch in GetComponentsInChildren<SetActiveCharacter>())
        {
            ch.SetCharacterState(ch.transform.GetSiblingIndex() == whichCharacter); // enable only one char with index whichCharacter
        }
    }
}