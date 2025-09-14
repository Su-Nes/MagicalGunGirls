using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> unlockedCharacters = new();
    public int maxCharacters = 2, missionsCompleted;
    public float maxPlayerHealth = 100f;
    [SerializeField] private int mendingNectar;
    public int MendingNectar => mendingNectar;
    
    private GainCurrencyGraphic mendingNectarGraphic;

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

        mendingNectarGraphic = FindObjectOfType<GainCurrencyGraphic>();
    }

    public List<GameObject> UnlockedCharacters => unlockedCharacters;

    public void UnlockCharacter(GameObject character)
    {
        unlockedCharacters.Add(character);
    }

    public void AddMendingNectar(int amount)
    {
        mendingNectarGraphic.AddCurrency(mendingNectar, mendingNectar + amount);
        mendingNectar += amount;
        print($"Adding {amount} MN, current nectar amount is: {mendingNectar}.");
    }
}
