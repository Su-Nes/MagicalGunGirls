using UnityEngine.Events;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Attack base parameters: ")] 
    [SerializeField] protected bool attackEnabled = true;
    [SerializeField] protected float timeBetweenAttacks = .1f;
    [Tooltip("If ya' want multiple attack sources to fire with alternate timings, switch this on.")]
    [SerializeField] protected bool fireOffset;
    [SerializeField] protected string attackInputName = "Fire1";
    private float attackTimer;
    
    [Header("Usage managers: ")]
    [SerializeField] private AmmoManager _ammoManager;
    [SerializeField] private int ammoSpentPerShot = 1;
    [SerializeField] private bool resetReloadTimeOnFire;
    [SerializeField] private CooldownManager _cooldownManager;
    [SerializeField] private float cooldownTime = 5f;

    [SerializeField] private UnityEvent fireEvent;
    

    private void Start()
    {
        if (fireOffset)
            attackTimer = timeBetweenAttacks / 2f;
    }

    protected virtual void Update()
    {
        if (GameManager.Instance.FreezePlayer)
            return;
        
        if (!attackEnabled || attackInputName.Length <= 0)
            return;
        
        if (Input.GetButton(attackInputName))
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer < 0f)
            {
                AttackTriggered();
                attackTimer = timeBetweenAttacks;
                
                fireEvent.Invoke();
            }
        }
        
        if (Input.GetButtonUp(attackInputName))
        {
            AttackReleased();
        }
    }

    public void ReleaseAttack()
    {
        AttackReleased();
    }
    
    public void DisableAttack()
    {
        attackEnabled = false;
    }

    public void EnableAttack()
    {
        attackEnabled = true;
    }

    protected virtual void AttackTriggered()
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
    
    protected virtual void AttackReleased()
    {
        if (fireOffset)
            attackTimer = timeBetweenAttacks / 2f;
        else
            attackTimer = 0;
    }
}
