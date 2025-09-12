using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCurrency : MonoBehaviour
{
    [SerializeField] private int minAmount, maxAmount;
    [SerializeField] private AudioClip clip;
    
    
    public void AddMendingNectar()
    {
        DataPersistenceManager.Instance.mendingNectar += Random.Range(minAmount, maxAmount + 1);
        SFXManager.Instance.PlaySFXClip(clip, transform.position, .6f, 1.3f, 1.6f);
    }
}
