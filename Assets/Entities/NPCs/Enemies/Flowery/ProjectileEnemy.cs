using UnityEngine;

public class ProjectileEnemy : EnemyAI
{
    [Header("Projectile params: ")]
    [SerializeField] protected float firePeriod;
    [SerializeField] protected float fireRangeFromPlayer = 15f;
    private float fireTimer;
    [SerializeField] protected FireProjectile _fireProjectile;
    [SerializeField] protected Animator _projectileAnim;
    [SerializeField] protected float lookAheadMod;
    

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (Vector3.Distance(transform.position, target.position) > fireRangeFromPlayer)
            return;

        if (GameManager.Instance.FreezePlayer)
            return; // don't fucking fire!!
        
        if (fireTimer < firePeriod)
            fireTimer += Time.deltaTime;
        else
        {
            Vector3 aimTarget = new Vector3(target.position.x, _fireProjectile.transform.position.y, target.position.z) +
                                target.GetComponent<PlayerMovement>().Direction().normalized * lookAheadMod;
            _fireProjectile.transform.LookAt(aimTarget);
                
            _projectileAnim.SetTrigger("fire");
            fireTimer = 0f;
        }
    }
}
