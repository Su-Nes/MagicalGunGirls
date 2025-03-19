using UnityEngine;

public class ExplosionJumpAbility : RepelAbility
{
    [Header("Explo jumpo:")]
    [SerializeField] private float verticalVelocity = 10f;
    
    private PlayerMovement playerController;


    private void Awake()
    {
        playerController = FindObjectOfType<PlayerMovement>();
    }

    protected override void ActivateAbility()
    {
        base.ActivateAbility();

        playerController.AddImpulseForce(Vector3.up * verticalVelocity);
    }
}
