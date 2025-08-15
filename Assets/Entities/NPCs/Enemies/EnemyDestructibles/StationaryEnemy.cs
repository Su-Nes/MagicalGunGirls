using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class StationaryEnemy : EnemyAI
{
    [SerializeField] private Image healthBar;
    [SerializeField] protected Renderer[] renderers;
    private Material defaultMat;
    [SerializeField] private Material flickerMat;
    [SerializeField] protected float flickerDuration;

    [SerializeField] private UnityEvent eventOnFirstAttack, eventOnDeath;
    private bool hasBeenAttacked;

    private void Start()
    {
        maxHealth = health;
        healthBar.fillAmount = health / maxHealth;
    }

    public override void TakeDamage(float healthValue)
    {
        if (!hasBeenAttacked)
        {
            eventOnFirstAttack.Invoke();
            hasBeenAttacked = true;
        }
        
        health -= healthValue;
        StartCoroutine(RenderMaterialFlicker());
        healthBar.fillAmount = health / maxHealth;
    }
    
    private IEnumerator RenderMaterialFlicker()
    {
        foreach (Renderer rend in renderers)
        {
            rend.material = flickerMat;
        }
        
        yield return new WaitForSeconds(flickerDuration);
        
        foreach (Renderer rend in renderers)
        {
            rend.material = defaultMat;
        }
    }
    
    protected override void Die()
    {
        eventOnDeath.Invoke();
        
        ObjectPoolManager.SpawnObject(deathParticles, transform.position, deathParticles.transform.rotation,
            ObjectPoolManager.PoolType.GameObject);
        
        GetComponent<PlayRandomSound>().Play();
        
        Destroy(gameObject);
    }
}
