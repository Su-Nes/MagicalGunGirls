using UnityEngine.Events;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("INSERT STATS OBJECT")]
    [SerializeField] private Stats characterStatSO;
    [Header("END STATS OBJECT")]
    
    [Header("Attack base parameters: ")] 
    [SerializeField] protected bool attackEnabled = true;
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

    [Header("Visual: ")]
    [SerializeField] private Animator animator;
    [SerializeField] private string animationName;
    
    
    private void Start()
    {
        print(transform.root);
        if (fireOffset)
            attackTimer = characterStatSO.fireDelay / 2f;
    }
    
    protected virtual void Update()
    {
        attackTimer -= Time.deltaTime; // cooldown between shots
        
        if (GameManager.Instance.FreezePlayer)
            return;
        
        if (!attackEnabled || attackInputName.Length <= 0)
            return;
        
        if (fireOffset && Input.GetButtonDown(attackInputName))
            attackTimer = characterStatSO.fireDelay / 2f;
        
        if (Input.GetButton(attackInputName))
        {
            if (attackTimer < 0f)
            {
                AttackTriggered();
                AttackReleased();
                
                fireEvent.Invoke();
            }
        }
    }
    
    public void DisableAttack()
    {
        attackEnabled = false;
    }

    public void EnableAttack()
    {
        //attackTimer = 0f;
        attackEnabled = true;
    }

    protected virtual void AttackTriggered()
    {
        if (animator != null)
        {
            animator.Play(animationName);
        }
        
        if (_ammoManager != null)
        {
            _ammoManager.SpendBullet(ammoSpentPerShot);
            if(resetReloadTimeOnFire)
                _ammoManager.ResetTimer();
        }
        
        if (_cooldownManager != null)
            _cooldownManager.TriggerCooldown(cooldownTime * characterStatSO.cooldownModifier);
    }
    
    protected virtual void AttackReleased()
    {
        attackTimer = characterStatSO.fireDelay;
    }
}
