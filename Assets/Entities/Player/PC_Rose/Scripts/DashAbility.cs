using UnityEngine;

public class DashAbility : Ability
{
    [Header("Dash!")]
    [SerializeField] private float dashForce = 7f;
    
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    protected override void ActivateAbility()
    {
        base.ActivateAbility();
        
        playerMovement.AddImpulseForce(playerMovement.Direction() * dashForce);
    }
}
