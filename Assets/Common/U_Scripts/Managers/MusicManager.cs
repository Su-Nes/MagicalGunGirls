using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] musicTracks;
    private int trackIndex;

    [SerializeField] private float fadeSpeed = .5f;
    
    private static MusicManager _instance;
    public static MusicManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
    }

    
    public void FadeToNextTrack()
    {
        Fade(musicTracks[trackIndex], GetComponent<AudioSource>().volume);
        trackIndex++;
    }
    
    private void Fade(AudioClip clip, float volume)
    {
        StartCoroutine(FadeIt(clip, volume));
    }
    
    private IEnumerator FadeIt(AudioClip clip, float volume)
    {//Add new audiosource and set it to all parameters of original audiosource
        AudioSource fadeOutSource = gameObject.AddComponent<AudioSource>();
        fadeOutSource.clip = GetComponent<AudioSource>().clip;
        fadeOutSource.time = GetComponent<AudioSource>().time;
        fadeOutSource.volume = GetComponent<AudioSource>().volume;
        fadeOutSource.outputAudioMixerGroup = GetComponent<AudioSource>().outputAudioMixerGroup;

        //make it start playing
        fadeOutSource.Play();

        //set original audiosource volume and clip
        GetComponent<AudioSource>().volume = 0f;
        GetComponent<AudioSource>().clip = clip;
        float t = 0;
        float v = fadeOutSource.volume;
        GetComponent<AudioSource>().Play();

        //begin fading in original audiosource with new clip as we fade out new audiosource with old clip
        while (t < 0.98f)
        {
            t = Mathf.Lerp(t, 1f, Time.deltaTime * fadeSpeed);
            fadeOutSource.volume = Mathf.Lerp(v, 0f, t);
            GetComponent<AudioSource>().volume = Mathf.Lerp(0f, volume, t);
            yield return null;
        }
        GetComponent<AudioSource>().volume = volume;
        //destroy the fading audiosource
        Destroy(fadeOutSource);
    }
}
