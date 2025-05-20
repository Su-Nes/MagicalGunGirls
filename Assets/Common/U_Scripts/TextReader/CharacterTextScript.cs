using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterTextScript : MonoBehaviour
{
    public bool playOnAwake;
    public float charWriteDelay = .1f;
    public float fastWriteDelay = .001f;
    private float startCharDelay;
    public int maxCharsPerLine = 30;

    public bool autoClear = true;
    public bool backspaceClear = false;
    public bool clearLineChars = false;
    public float clearWaitTime = 2f;
    public float charClearDelay = .01f;

    public AudioSource voiceAudio;

    [HideInInspector]
    public TMP_Text text;
    [HideInInspector]
    public List<char> chars = new();
    [HideInInspector]
    public bool isWriting;


    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        
        startCharDelay = charWriteDelay;
        
        if (playOnAwake)
            ReadText(text.text);
        else
            text.text = "";
    }

    private int lines = 1;
    public void ReadText(string inputText)
    {
        StopAllCoroutines();
        text.text = "";
        chars.Clear();
        lines = 1;

        int lineChars = 0;
        foreach (char ch in inputText) //add line breaks 
        {            
            chars.Add(ch);
            lineChars++;

            if (ch == '\n')
            {
                lines++;
                lineChars = 0;
            }
            
            if (lineChars >= maxCharsPerLine && ch == ' ')
            {
                chars.Add('\n');

                lines++;
                lineChars = 0;
            }
        }

        StartCoroutine(WriteChars());
    }

    
    public void FastWrite()
    {
        charWriteDelay = fastWriteDelay;
    }

    private IEnumerator WriteChars()
    {
        isWriting = true;
        if(voiceAudio != null)
            voiceAudio.mute = false;
        
        int i = 0;
        string writtenText = "";
        foreach (char ch in chars)
        {
            writtenText += ch;

            if (ch == '\n' && lines > 0)
                lines--;

            text.text = writtenText + new string('\n', lines); //while text is written, keep line breaks for the whole text
            yield return new WaitForSeconds(charWriteDelay);   //looks better when text is aligned left of right
            
            i++;
            if (i > chars.Count - 1)
                break;
        }

        chars.Clear();
        if(autoClear)
            StartCoroutine(ClearChars());

        if(voiceAudio != null)
            voiceAudio.mute = true;
        charWriteDelay = startCharDelay;
        isWriting = false;
    }

    public IEnumerator ClearChars()
    {
        yield return new WaitForSeconds(clearWaitTime);
        if (backspaceClear)
        {
            int charRemoveIndex = 2;
            while (text.text.Length > 0)
            {
                if(text.text.Length - charRemoveIndex >= 0)
                {//to do: make it so the line breaks don't clear
                    if (text.text[text.text.Length - charRemoveIndex] == '\n' && clearLineChars)
                        charRemoveIndex++;
                    text.text = text.text.Remove(text.text.Length - charRemoveIndex);
                }
                else
                    text.text = text.text.Remove(0);
                yield return new WaitForSeconds(charClearDelay);
            }
        }else
        {
            text.text = "";
        }
    }
}
