using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [Header("Usage managers: ")]
    [SerializeField] private AmmoManager _ammoManager;
    [SerializeField] private int ammoSpentPerShot = 1;
    [SerializeField] private CooldownManager _cooldownManager;
    [SerializeField] private float cooldownTime = 5f;
    
    
    protected virtual void Update()
    {
        if (Input.GetButtonDown("Ability") || Input.GetButtonDown("Jump"))
        {
            ActivateAbility();
        }
    }

    protected virtual void ActivateAbility()
    {
        if (_ammoManager != null)
            _ammoManager.SpendBullet(ammoSpentPerShot);
        
        if (_cooldownManager != null)
            _cooldownManager.TriggerCooldown(cooldownTime);
    }
}
