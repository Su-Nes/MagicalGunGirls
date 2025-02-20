using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerDamagePlayer : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float damageInterval = 1f;
    private PlayerStatsManager player;
    private float damageTimer;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerStatsManager p))
        {
            player = p;
        }
    }

    private void Update()
    {
        if (player != null)
        {
            if (damageTimer <= 0f)
            {
                damageTimer = damageInterval;
                player.ModifyHealthValue(-damageAmount);
            }

            damageTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerStatsManager p))
        {
            damageTimer = 0f;
            player = null;
        }
    }
}
