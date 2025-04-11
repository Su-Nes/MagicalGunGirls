using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class BaseCampSequence : MonoBehaviour
{
    [SerializeField] private string openingDialogue;
    
    private void Start()
    {
        FindObjectOfType<DialogueRunner>().StartDialogue(openingDialogue);
    }
    
    
}
