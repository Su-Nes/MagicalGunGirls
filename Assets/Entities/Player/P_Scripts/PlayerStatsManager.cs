using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using Yarn.Unity;

public class PlayerStatsManager : MonoBehaviour
{
    private bool isDead;
    public bool Invincible;
    [SerializeField] private Image healthUIBar;
    [SerializeField] private TMP_Text healthUIText; 
    private float health;

    [Header("Hit effects: ")] 
    [SerializeField] private AudioClip takeHitSFX;
    [SerializeField] private float effectLerp = .3f;
    [SerializeField] private float chromeAbbMaxIntensity = .8f;
    [SerializeField] private float vignetteMaxIntensity = .8f;
    private float chromeAbbDefault, vignetteDefault;
    private ChromaticAberration chromeAbb;
    private Vignette vignette;
    private Volume volume;
    
    
    private void Start()
    {
        health = DataPersistenceManager.Instance.maxPlayerHealth;
        
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
        healthUIBar.fillAmount = health / DataPersistenceManager.Instance.maxPlayerHealth;
        healthUIText.text = $"{health}/{DataPersistenceManager.Instance.maxPlayerHealth}";

        if (health <= 0f && !isDead)
            Die();
    }

    [YarnCommand("AddHealth")]
    public void ModifyHealthValue(float add)
    {
        if (add < 0f)
        {
            if (Invincible)
                return;
            StartCoroutine(GetHitEffect());
        }
        
        health += add;
        if (health > DataPersistenceManager.Instance.maxPlayerHealth)
            health = DataPersistenceManager.Instance.maxPlayerHealth;

        if (health < 0f)
            health = 0f;
    }

    private IEnumerator GetHitEffect()
    {
        SFXManager.Instance.PlaySFXClip(takeHitSFX, transform.position, 1f, .9f, 1f);
        
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
        isDead = true;
        GameManager.Instance.GameOver();
    }
}
