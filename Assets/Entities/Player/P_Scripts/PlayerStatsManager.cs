using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStatsManager : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Image healthUI;
    private float health;
    
    
    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        healthUI.fillAmount = health / maxHealth;
        
        if (health <= 0f)
            Die();
    }

    public void ModifyHealthValue(float add)
    {
        health += add;
    }

    private void Die()
    {
        GameManager.Instance.GameOver();
    }
}
