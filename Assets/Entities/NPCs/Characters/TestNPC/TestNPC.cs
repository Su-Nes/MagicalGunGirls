using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class TestNPC : Interactable
{
    [SerializeField] private string startDialogue;
    
    public override void OnInteract()
    {
        FindObjectOfType<DialogueRunner>().StartDialogue(startDialogue);
    }
}
