using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FireProjectile))]
public class JumpBombAbility : Ability
{
    [Header("Explo jumpo:")]
    [SerializeField] private float verticalVelocity = 10f;
    [SerializeField] private float bombDropRecoil = 2f;
    [SerializeField] private float bombDropDelay = .7f;
    
    private FireProjectile bombLauncher;
    private PlayerMovement playerController;
    


    private void Awake()
    {
        playerController = FindObjectOfType<PlayerMovement>();
        bombLauncher = GetComponent<FireProjectile>();
    }

    protected override void ActivateAbility()
    {
        base.ActivateAbility();

        StartCoroutine(JumpBombDrop());
    }

    private IEnumerator JumpBombDrop()
    {
        playerController.AddImpulseForce(Vector3.up * verticalVelocity); // jump
        
        yield return new WaitForSeconds(bombDropDelay);
        
        playerController.AddImpulseForce(Vector3.up * bombDropRecoil); // recoil
        bombLauncher.FireProjectilePublic(); // bomb
    }
}
