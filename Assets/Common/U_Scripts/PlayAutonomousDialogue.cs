using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class PlayAutonomousDialogue : MonoBehaviour
{
    public void PlayNextDialogue(string dialogue)
    {
        StartCoroutine(JamTheDialoguesInARow(dialogue));
    }

    private IEnumerator JamTheDialoguesInARow(string dialogue)
    {
        FindObjectOfType<DialogueRunner>().Stop();
        yield return new WaitForSeconds(1f);
        FindObjectOfType<DialogueRunner>().StartDialogue(dialogue);
    }
}
