using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayRandomSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] sounds;
    [SerializeField] private float SFXVolume = 1f;
    [SerializeField] private float minPitch = .9f, maxPitch = 1.1f;
    
    public void Play()
    {
        SFXManager.Instance.PlaySFXClip(sounds[Random.Range(0, sounds.Length)], transform.position, SFXVolume, minPitch, maxPitch);
    }
}
