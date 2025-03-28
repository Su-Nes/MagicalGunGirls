using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionDamagePlayer : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float damageInterval = 1f;
    [SerializeField] private bool destroyOnHit;
    private PlayerStatsManager player;
    private float damageTimer;
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out PlayerStatsManager p))
        {
            player = p;
        }else if (destroyOnHit && !other.gameObject.CompareTag("Enemy"))
            Destroy(gameObject);
    }

    private void Update()
    {
        if (player != null)
        {
            if (damageTimer <= 0f)
            {
                damageTimer = damageInterval;
                player.ModifyHealthValue(-damageAmount);
                
                if(destroyOnHit)
                    Destroy(gameObject);
            }

            damageTimer -= Time.deltaTime;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.TryGetComponent(out PlayerStatsManager p))
        {
            damageTimer = 0f;
            player = null;
        }
    }
}
