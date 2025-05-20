using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine;
using TMPro;


public class DialogueReadScript : MonoBehaviour
{
    [Header("Script assigns:")]
    [SerializeField] private CharacterTextScript textReader;
    private List<string> currentDialogue = new();
    public UnityEvent dialogueEvent;

    [Header("Object assigns:")]
    public GameObject panel;
    public TMP_FontAsset playerFont, kidnapperFont;
    public AudioSource playerVoice, kidnapperVoice;
    public Color playerFontColour, kidnapperFontColour;

    private int lineIndex;
    private int activationListIndex;
    private bool triggerActive;

    public void NextLine()
    {// textReader.chars.Count == 0 && 
        if (lineIndex < currentDialogue.Count)
        {
            if (!textReader.isWriting)
                ReadDialogue();
            else
                SkipReading();
        }
        else if(lineIndex >= currentDialogue.Count)// if the reader is done reading, read next line
            EndDialogue();
    }

    public void StartDialogue(string[] dialogue)
    {
        EndDialogue();

        // make list
        foreach (string line in dialogue)
        {
            currentDialogue.Add(line);
        }

        StartCoroutine(ReadFirstLine()); // coroutine because there's something weird about it not calling the NextLine() function here
    }

    private void ReadDialogue()
    {
        string[] readText = currentDialogue[lineIndex].Split('`');
        switch (readText[0])// read what text type it is
        {
            case "p":// player dialogue
                textReader.text.font = playerFont;
                textReader.text.color = playerFontColour;
                textReader.voiceAudio = playerVoice;
                break;
            case "pi"://player inner dialogue
                //textReader.text.fontStyle = FontStyles.Italic;
                print("not implemented");
                break;
            case "n":// npc dialogue
                textReader.text.font = kidnapperFont;
                textReader.text.color = kidnapperFontColour;
                textReader.voiceAudio = kidnapperVoice;
                break;
            case "st":// load next scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                return;
        }

        if (readText.Length > 2)
        {
            switch (readText[2])
            {
                case "ev":
                    StartCoroutine(EventTimer(float.Parse(readText[3])));
                    break;
            }
        }
        
        textReader.ReadText(readText[1]);// read the main part of the text
        
        lineIndex++;
    }

    private void SkipReading()
    {
        textReader.FastWrite();
    }

    private void EndDialogue()
    {
        textReader.StartCoroutine(textReader.ClearChars()); // eeehhhhhh whatever
        textReader.StopAllCoroutines();
        textReader.isWriting = false;
        textReader.voiceAudio.mute = true; // failsafe
        
        lineIndex = 0;
        currentDialogue.Clear();

        panel.SetActive(false);
    }

    private IEnumerator ReadFirstLine()
    {
        yield return new WaitForSeconds(1f);
        panel.SetActive(true); // activate UI
        NextLine();
    }

    private IEnumerator EventTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        dialogueEvent.Invoke();
    }
}
