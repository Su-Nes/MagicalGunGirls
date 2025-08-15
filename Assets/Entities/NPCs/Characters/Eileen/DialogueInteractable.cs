using UnityEngine;
using Yarn.Unity;

public class DialogueInteractable : Interactable
{
    [SerializeField] private string[] dialogueOrder;
    private int dialogueIndex;
    
    public override void OnInteract()
    {
        FindObjectOfType<DialogueRunner>().StartDialogue(dialogueOrder[dialogueIndex]);
        dialogueIndex++;
        if (dialogueIndex > dialogueOrder.Length - 1)
            dialogueIndex = 0;
    }
}
