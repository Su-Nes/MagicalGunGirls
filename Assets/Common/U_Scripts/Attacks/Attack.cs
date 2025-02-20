using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Attack base parameters: ")] 
    [SerializeField] protected float timeBetweenAttacks = .1f;
    [Tooltip("If ya' want multiple attack sources to fire with alternate timings, switch this on.")]
    [SerializeField] protected bool fireOffset;
    [SerializeField] protected bool disablePrimaryAttack, disableSecondaryAttack;
    private float attackTimer;
    
    [Header("Usage managers: ")]
    [SerializeField] private AmmoManager _ammoManager;
    [SerializeField] private int ammoSpentPerShot = 1;
    [SerializeField] private bool resetReloadTimeOnFire;
    [SerializeField] private CooldownManager _cooldownManager;
    [SerializeField] private float cooldownTime = 5f;
    
    private bool attackingPrimary, attackingSecondary;

    [SerializeField] private UnityEvent fireEvent;
    

    private void Start()
    {
        if (fireOffset)
            attackTimer = timeBetweenAttacks / 2f;
        else
            attackTimer = timeBetweenAttacks;
    }

    protected virtual void Update()
    {
        if (Input.GetButton("Fire1") && !attackingPrimary)
        {
            if (!disablePrimaryAttack)
            {
                attackingPrimary = true;
                PrimaryAttackPrimed();
            }
        }
        
        if (Input.GetButtonUp("Fire1"))
        {
            if (!disablePrimaryAttack)
            {
                PrimaryAttackReleased();
                attackingPrimary = false;
            }
        }

        if (Input.GetButton("Fire2") && !attackingSecondary)
        {
            if (!disableSecondaryAttack)
            {
                attackingSecondary = true;
                SecondaryAttackPrimed();
            }
        }
        
        if (Input.GetButtonUp("Fire2"))
        {
            
            if (!disableSecondaryAttack)
            {
                attackingSecondary = false;
                SecondaryAttackReleased();
            }
        }
        
        if (attackingPrimary)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= timeBetweenAttacks)
            {
                PrimaryAttackTriggered();
                attackTimer = 0f;
                
                fireEvent.Invoke();
            }
        }
    }

    public void ReleaseAllAttacks()
    {
        attackingPrimary = false;
        attackingSecondary = false;
        PrimaryAttackReleased();
        //SecondaryAttackReleased(); this function doesn't do what it's named because this line caused a stack overflow with the ReadyWeapon script
    }

    protected virtual void PrimaryAttackPrimed()
    {
        
    }

    protected virtual void PrimaryAttackTriggered()
    {
        if (_ammoManager != null)
        {
            _ammoManager.SpendBullet(ammoSpentPerShot);
            if(resetReloadTimeOnFire)
                _ammoManager.ResetTimer();
        }
        
        if (_cooldownManager != null)
            _cooldownManager.TriggerCooldown(cooldownTime);
    }
    
    protected virtual void PrimaryAttackReleased()
    {
        if (fireOffset)
            attackTimer = timeBetweenAttacks / 2f;
        else
            attackTimer = timeBetweenAttacks;
    }
    
    protected virtual void SecondaryAttackPrimed()
    {
        print("Kee! " + "from object " + name);
    }
    
    protected virtual void SecondaryAttackTriggered()
    {
        if (_ammoManager != null)
            _ammoManager.SpendBullet(ammoSpentPerShot);
        
        if (_cooldownManager != null)
            _cooldownManager.TriggerCooldown(cooldownTime);
    }
    
    protected virtual void SecondaryAttackReleased()
    {
        
    }
}
