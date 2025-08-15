using UnityEngine;

public class Ability : MonoBehaviour
{
    [Header("INSERT STATS OBJECT")]
    [SerializeField] private Stats characterStatSO;
    [Header("END STATS OBJECT")]
    
    [Header("Usage managers: ")] 
    [SerializeField] private AmmoManager _ammoManager;
    [SerializeField] private int ammoSpentPerShot = 1;
    [SerializeField] private CooldownManager _cooldownManager;
    [SerializeField] private float cooldownTime = 5f;
    
    [Header("Effects: ")]
    [SerializeField] private Animator animator;
    [SerializeField] private string animationName;
    [SerializeField] private AudioClip SFX;
    [SerializeField] private float SFXVolume = 1f;
    [SerializeField] private float minPitch = .9f, maxPitch = 1.1f;
    
    
    protected virtual void Update()
    {
        if (GameManager.Instance.FreezePlayer)
            return;
        
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
            _cooldownManager.TriggerCooldown(cooldownTime * characterStatSO.cooldownModifier);

        if(SFX != null)
            SFXManager.Instance.PlaySFXClip(SFX, transform.position, SFXVolume, minPitch, maxPitch);
        
        if(animator != null)
            animator.Play(animationName);
    }
}
