using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Yarn.Unity;

public class CharacterSwap : MonoBehaviour
{
    [SerializeField] private TMP_Text teamSizeText;
    [SerializeField] private List<GameObject> characterPrefabs;
    public int CharacterCount { get { return characterPrefabs.Count; } }
    [SerializeField] private Transform teamDisplayParent;
    [SerializeField] private GameObject panelUIObj, charNameDisplayText;
    private Transform UIParent;
    private GameObject currentCharacter;
    private int whichCharacter;

    private void Awake()
    {
        UIParent = GameObject.Find("CharacterSelectLayout").transform;
    }

    private void Start()
    {
        InitialiseCharacters();
        
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

        if (Input.inputString.Length > 0 && int.TryParse(Input.inputString, out int inputInt))
        {
            Swap(inputInt - 1);
        }
    }

    private void InitialiseCharacters()
    {
        teamSizeText.text = $"Max team size: {DataPersistenceManager.Instance.maxCharacters}";

        foreach (Transform child in UIParent)
        {
            Destroy(child.gameObject);
        }
        
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        foreach (Transform child in teamDisplayParent)
        {
            Destroy(child.gameObject);
        }
        
        foreach (GameObject ch in characterPrefabs)
        {
            Instantiate(panelUIObj, Vector3.zero, Quaternion.identity, UIParent);
            Instantiate(ch, transform.position, transform.rotation, transform);
            TMP_Text charName = Instantiate(charNameDisplayText, Vector3.zero, Quaternion.identity, teamDisplayParent).GetComponent<TMP_Text>();
            charName.text = ch.GetComponent<CharacterUIManager>().CharacterName;
        }
        
        Swap(transform.childCount - 1);
        whichCharacter = transform.childCount - 2;
    }

    public void AddCharacter(GameObject character)
    {
        if (characterPrefabs.Count >= DataPersistenceManager.Instance.maxCharacters)
            return;
        
        characterPrefabs.Add(character);
        characterPrefabs = characterPrefabs.Distinct().ToList();
        InitialiseCharacters();
    }
    
    public void AddCharacter(GameObject character, bool increaseCharacterLimit)
    {
        if (increaseCharacterLimit)
            DataPersistenceManager.Instance.maxCharacters++;
        
        if (characterPrefabs.Count >= DataPersistenceManager.Instance.maxCharacters)
            return;
        
        characterPrefabs.Add(character);
        InitialiseCharacters();
        
        if (characterPrefabs.Count > 0)
        {
            whichCharacter = 0;
            Swap();
        }
    }

    public void RemoveCharacter(GameObject character)
    {
        characterPrefabs.Remove(character);
        InitialiseCharacters();
    }

    public void ClearTeam()
    {
        characterPrefabs.Clear();
        InitialiseCharacters();
    }

    private void Swap()
    {
        foreach (SetActiveCharacter ch in GetComponentsInChildren<SetActiveCharacter>())
        {
            ch.SetCharacterState(false);
            ch.SetCharacterState(ch.transform.GetSiblingIndex() == whichCharacter); // enable only one char with index whichCharacter
        }
    }
    
    private void Swap(int characterIndex)
    {
        if (characterIndex > GetComponentsInChildren<SetActiveCharacter>().Length - 1)
            return;
            
        foreach (SetActiveCharacter ch in GetComponentsInChildren<SetActiveCharacter>())
        {
            ch.SetCharacterState(ch.transform.GetSiblingIndex() == characterIndex); // enable only one char with index whichCharacter
        }
    }

    public void ResetCharacter()
    {
        Swap();
    }
}