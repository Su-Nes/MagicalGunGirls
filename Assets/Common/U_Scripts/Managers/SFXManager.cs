using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    private static SFXManager _instance;
    public static SFXManager Instance { get { return _instance; } }

    [SerializeField] private GameObject SFXObject;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
    }
    
    public void PlayRandomSFX(AudioClip[] audioClips, Vector3 spawnPosition, float volume)
    {
        PlaySFXClip(audioClips[Random.Range(0, audioClips.Length)], spawnPosition, volume);
    }
    
    public void PlaySFXClip(AudioClip audioClip, Vector3 spawnPosition, float volume, float minPitch = 1f, float maxPitch = 1f)
    {
        AudioSource audioSource = ObjectPoolManager.SpawnObject(SFXObject, spawnPosition, Quaternion.identity, ObjectPoolManager.PoolType.AudioSource).GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        StartCoroutine(RemoveAudioSource(audioSource.gameObject, clipLength));
    }

    private IEnumerator RemoveAudioSource(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        if(obj != null)
            ObjectPoolManager.ReturnObjectToPool(obj);
    }
}
