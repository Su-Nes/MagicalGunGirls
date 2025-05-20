using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StingerAbility : Ability
{
    [SerializeField] private float dashForce = 7f, damageDuration = .25f;
    [SerializeField] private OnTriggerDamageEnemy stinger;
    private PlayerMovement playerMovement;
    private PlayerStatsManager playerStats;

    
    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerStats = FindObjectOfType<PlayerStatsManager>();
    }

    protected override void ActivateAbility()
    {
        base.ActivateAbility();

        StartCoroutine(Stinger());
    }

    private IEnumerator Stinger()
    {
        stinger.transform.LookAt(transform.position + playerMovement.Direction());

        playerStats.Invincible = true;
        playerMovement.AddImpulseForce(playerMovement.Direction() * dashForce);
        stinger.gameObject.SetActive(true);

        yield return new WaitForSeconds(damageDuration);
        playerStats.Invincible = false;
        stinger.gameObject.SetActive(false);
    }
}
