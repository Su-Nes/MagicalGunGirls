using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> unlockedCharacters = new();
    public int maxCharacters = 2, mendingNectar, missionsCompleted;
    public float maxPlayerHealth = 100f;
    
    private static DataPersistenceManager _instance;
    public static DataPersistenceManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public List<GameObject> UnlockedCharacters
    {
        get { return unlockedCharacters; }
    }
    
    public void UnlockCharacter(GameObject character)
    {
        unlockedCharacters.Add(character);
    }
}
