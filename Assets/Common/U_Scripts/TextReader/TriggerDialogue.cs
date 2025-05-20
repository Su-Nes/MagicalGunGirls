using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    public bool onSceneLoad;
    
    public DialogueReadScript dialogueScript;
    [TextArea(2, 5)]
    public string[] dialogue;
    
    
    private void Start()
    {
        if (onSceneLoad)
            StartDialogue();
    }
    
    private void StartDialogue()
    {
        dialogueScript.StartDialogue(dialogue);
        Destroy(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            StartDialogue();
    }
}
