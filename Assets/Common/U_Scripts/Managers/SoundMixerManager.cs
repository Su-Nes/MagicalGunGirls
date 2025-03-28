using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(level) * 20f);
    }

    public void SetSoundVolume(float level)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(level) * 20f);
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);
    }

    /*private float GetVolumeValue(float startVolume)
    {
        float volume = Mathf.Pow(startVolume, 2.7f); // make the volume control feel better. 2.7 is euler's number, the sweet spot.
        return ExtensionMethods.Remap(volume, 0f, 1f, -80f, 0f);
    }*/
}
