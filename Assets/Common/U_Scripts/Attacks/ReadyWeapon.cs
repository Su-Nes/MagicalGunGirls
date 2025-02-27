using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ReadyWeapon : Attack // this script needs rewriting
{
    [Header("Weapon charge params: ")]
    [SerializeField] private Animator _animator;
    [SerializeField] private AmmoManager overwriteReload;
    [SerializeField] private FireProjectile beamProjectile;
    [SerializeField] private string aimParamName;

    [SerializeField] private UnityEvent primeEvent, releaseEvent;

    [SerializeField] private float readyTime;
    private float timer;
    
    private bool readying;
    
    
    protected override void Update()
    {
        if (!attackEnabled)
            return;
        
        if (Input.GetButton(attackInputName))
        {
            if (!readying) // what used to be AttackPrimed()
            {
                primeEvent.Invoke();
                
                if (overwriteReload != null)
                {
                    overwriteReload.reloadOverwritten = true;
                    overwriteReload.DisableAllManagedAttacks();
                }
        
                _animator.SetBool(aimParamName, true);
                readying = true;
            
                timer = 0f;
            }else // charge attack
            {
                if (overwriteReload != null)
                    overwriteReload.reloadOverwritten = true;
            
                timer += Time.deltaTime;
            }
        }
        
        if (Input.GetButtonUp(attackInputName))
        {
            AttackReleased();
        }
    }

    protected override void AttackReleased()
    {
        base.AttackTriggered(); // cooldown triggers
        
        readying = false;
        _animator.SetBool(aimParamName, false);
        releaseEvent.Invoke();
        
        if (overwriteReload != null)
        {
            overwriteReload.reloadOverwritten = false;
            overwriteReload.OverwriteEnableAttacks();
        }
        
        if (timer >= readyTime)
        {
            beamProjectile.FireProjectilePublic();
        }
    }
}
