using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected float interactIconFloatHeight = 1.5f;
        
    
    private void Awake()
    {
        gameObject.layer = 10;
    }
    
    public Vector3 InteractIconPosition()
    {
        return transform.position + Vector3.up * interactIconFloatHeight;
    }

    public abstract void OnInteract();
}
