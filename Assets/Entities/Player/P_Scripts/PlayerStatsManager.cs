using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

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

    [YarnCommand("AddHealth")]
    public void ModifyHealthValue(float add)
    {
        health += add;
        if (health > maxHealth)
            health = maxHealth;
    }

    private void Die()
    {
        GameManager.Instance.GameOver();
    }
}
