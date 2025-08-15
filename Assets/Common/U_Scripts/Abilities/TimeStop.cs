using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStop : Ability
{
    [Header("Zawarudo!!")]
    [SerializeField] private float duration;
    [SerializeField] private GameObject UIOverlay;
    [SerializeField] private AudioSource startSFX, endSFX;
    
    
    protected override void ActivateAbility()
    {
        base.ActivateAbility();

        StartCoroutine(StopTime());
    }

    private IEnumerator StopTime()
    {
        GameManager.Instance.FreezeEnemies = true;
        UIOverlay.SetActive(true);
        startSFX.mute = false;
        startSFX.Play();
        
        yield return new WaitForSeconds(duration);
        
        GameManager.Instance.FreezeEnemies = false;
        UIOverlay.SetActive(false);
        startSFX.mute = true;
        endSFX.Play();
    }
}
