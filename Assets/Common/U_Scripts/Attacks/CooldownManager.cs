using UnityEngine;
using UnityEngine.UI;

public class CooldownManager : AttackManager
{
    [Header("Cooldown params: ")]
    [SerializeField] private Image cooldownUI;
    private float cooldownTime = 1;
    private float timer;


    private void Start()
    {
        timer = cooldownTime;
        EnableAttacks();
        EnableAbilities();
    }

    private void Update()
    {
        cooldownUI.fillAmount = FillAmount();
        
        if (timer < cooldownTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            EnableAttacks();
            EnableAbilities();
        }
    }

    public float FillAmount()
    {
        return timer / cooldownTime;
    }

    public void TriggerCooldown(float time)
    {
        DisableAttacks();
        DisableAbilities();
        cooldownTime = time;
        timer = 0f;
    }
}
