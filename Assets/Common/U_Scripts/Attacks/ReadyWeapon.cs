using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ReadyWeapon : Attack
{
    [Header("Weapon charge params: ")]
    private Animator _animator;
    [SerializeField] private AmmoManager overwriteReload;
    [SerializeField] private FireProjectile _fireProjectile;
    [SerializeField] private string aimParamName;

    [SerializeField] private UnityEvent primeEvent, releaseEvent;

    [SerializeField] private float readyTime;
    private float timer;
    
    private bool readying;
    
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    protected override void Update()
    {
        base.Update(); // need this to get the inputs

        if (readying)
        {
            if (overwriteReload != null)
                overwriteReload.reloadOverwritten = true;
            
            timer += Time.deltaTime;
            
            ReleaseAllAttacks();
        }
        else
            timer = 0f;
    }
    
    protected override void SecondaryAttackPrimed()
    {
        primeEvent.Invoke();
        
        _animator.SetBool(aimParamName, true);
        readying = true;
    }

    protected override void SecondaryAttackTriggered()
    {
        _fireProjectile.TriggerProjectilePublic();
    }

    protected override void SecondaryAttackReleased()
    {
        if (overwriteReload != null)
        {
            overwriteReload.reloadOverwritten = false;
            overwriteReload.OverwriteEnableAttacks();
            overwriteReload.ReleaseAllManagedAttacks();
        }
        
        _animator.SetBool(aimParamName, false);
        
        releaseEvent.Invoke();
        
        if (timer >= readyTime)
            SecondaryAttackTriggered();
        
        readying = false;
    }
}
