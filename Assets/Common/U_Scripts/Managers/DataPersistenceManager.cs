using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> unlockedCharacters = new();
    
    public List<GameObject> UnlockedCharacters
    {
        get { return unlockedCharacters; }
    }
    
    
    public void UnlockCharacter(GameObject character)
    {
        unlockedCharacters.Add(character);
    }
}
