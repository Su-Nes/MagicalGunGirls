using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using Yarn.Unity;

public class PlayerStatsManager : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Image healthUI;
    private float health;
    
    [Header("Hit effects: ")]
    [SerializeField] private float effectLerp = .3f;
    [SerializeField] private float chromeAbbMaxIntensity = .8f;
    [SerializeField] private float vignetteMaxIntensity = .8f;
    private float chromeAbbDefault, vignetteDefault;
    private ChromaticAberration chromeAbb;
    private Vignette vignette;
    private Volume volume;
    
    
    private void Start()
    {
        health = maxHealth;
        
        volume = FindObjectOfType<Volume>(); // god this is a pain in the ass
        volume.profile.TryGet(out ChromaticAberration ca);
        chromeAbb = ca;
        volume.profile.TryGet(out Vignette v);
        vignette = v;

        chromeAbbDefault = chromeAbb.intensity.value;
        vignetteDefault = vignette.intensity.value;
    }
    

    private void Update()
    {
        healthUI.fillAmount = health / maxHealth;
        
        if (health <= 0f)
            Die();
    }

    [YarnCommand("AddHealth")]
    public void ModifyHealthValue(float add)
    {
        if (add < 0f)
            StartCoroutine(GetHitEffect());
        
        health += add;
        if (health > maxHealth)
            health = maxHealth;
    }

    private IEnumerator GetHitEffect()
    {
        chromeAbb.intensity.value = chromeAbbMaxIntensity;
        vignette.intensity.value = vignetteMaxIntensity;
        
        while (!Mathf.Approximately(chromeAbb.intensity.value, chromeAbbDefault))
        {
            chromeAbb.intensity.value = Mathf.Lerp(chromeAbb.intensity.value, chromeAbbDefault, effectLerp);
            vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, vignetteDefault, effectLerp);
            yield return null;
        }
    }

    private void Die()
    {
        GameManager.Instance.GameOver();
    }
}
