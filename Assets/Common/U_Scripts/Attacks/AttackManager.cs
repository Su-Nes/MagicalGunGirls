using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] protected Attack[] managedAttacks;
    [SerializeField] protected Ability[] managedAbilities;
    
    
    protected virtual void EnableAttacks()
    {
        foreach (Attack attack in managedAttacks)
            attack.enabled = true;
    }

    public virtual void ReleaseAllManagedAttacks()
    {
        foreach (Attack attack in managedAttacks)
            attack.ReleaseAllAttacks();
    }

    protected virtual void DisableAttacks()
    {
        foreach (Attack attack in managedAttacks)
        {
            attack.ReleaseAllAttacks();
            attack.enabled = false;
        }
    }
    
    protected virtual void EnableAbilities()
    {
        foreach (Ability ability in managedAbilities)
            ability.enabled = true;
    }

    protected virtual void DisableAbilities()
    {
        foreach (Ability ability in managedAbilities)
        {
            ability.enabled = false;
        }
    }
    
    public void OverwriteEnableAttacks()
    {
        EnableAttacks();
    }
}
