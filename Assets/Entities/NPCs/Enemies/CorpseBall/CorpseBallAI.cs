using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class CorpseBallAI : EnemyAI
{
    [Header("Corpse ball params: ")]
    [SerializeField] private int splitBallCount = 3;
    [SerializeField] private GameObject splitBallObj;
    [SerializeField] private float ballExplodeForce = 12f;
    [SerializeField] private float ballSpawnDistance = .5f;
    
    [Header("Values if enemy is rendered with a 3D model: ")] 
    [SerializeField] protected Renderer[] renderers;
    private Material defaultMat;
    [SerializeField] private Material flickerMat;
    [SerializeField] protected float flickerDuration;
    
    private Rigidbody rb;
    
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        defaultMat = renderers[0].material;
    }
    
    protected override void FixedUpdate()
    {
        rb.isKinematic = GameManager.Instance.FreezePlayer;
        
        if (NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path))
        {
            Vector3 dir = path.corners[1] - transform.position;
            Vector3 dirFlat = new(dir.x, 0f, dir.z);
            
            Move(dirFlat.normalized, moveSpeed * Time.deltaTime);
        }
        
        if (health <= 0f)
            Die();
    }
    
    protected override void Move(Vector3 dir, float speed)
    {
        rb.AddForce(dir * speed);
    }
    
    public override void TakeDamage(float healthValue)
    {
        health -= healthValue;
        StartCoroutine(RenderMaterialFlicker());
    }
    
    private IEnumerator RenderMaterialFlicker()
    {
        foreach (Renderer rend in renderers)
        {
            rend.material = flickerMat;
        }
        
        yield return new WaitForSeconds(flickerDuration);
        
        foreach (Renderer rend in renderers)
        {
            rend.material = defaultMat;
        }
    }
    
    protected override void Die()
    {
        for (int i = splitBallCount; i > 0; i--)
        {
            Quaternion spawnAngle = Quaternion.Euler(0f, 360f / splitBallCount * i, 0f);
            Vector3 spawnPos = transform.position + spawnAngle * transform.forward * ballSpawnDistance;
            Rigidbody newBallRb = ObjectPoolManager.SpawnObject(splitBallObj, spawnPos, splitBallObj.transform.rotation, ObjectPoolManager.PoolType.GameObject).GetComponent<Rigidbody>();
            newBallRb.AddExplosionForce(ballExplodeForce, transform.position, ballExplodeForce);
        }
        
        foreach (Renderer rend in renderers)
        {
            rend.material = defaultMat;
        }
        
        base.Die();
    }
}
