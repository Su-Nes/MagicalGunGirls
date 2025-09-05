using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [Header("INSERT STATS OBJECT")]
    [SerializeField] protected Stats characterStatSO;
    [SerializeField] protected CharacterUIManager statUIManager;
    [Header("END STATS OBJECT")]
    
    [SerializeField] protected Attack[] managedAttacks;
    [SerializeField] protected Ability[] managedAbilities;
    
    
    protected virtual void EnableAttacks()
    {
        foreach (Attack attack in managedAttacks)
            attack.EnableAttack();
    }

    public virtual void DisableAllManagedAttacks()
    {
        DisableAttacks();
    }

    protected virtual void DisableAttacks()
    {
        foreach (Attack attack in managedAttacks)
        {
            attack.DisableAttack();
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
