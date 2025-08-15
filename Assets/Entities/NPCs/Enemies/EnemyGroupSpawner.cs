using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyGroupSpawner : MonoBehaviour
{
    public bool autonomous = true;
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private float initialSpawnDelay, spawnFrequency, spawnRadius, verticalPos;
    private bool initialSpawnComplete;
    private float timer;
    [SerializeField] private int groupSize, spawnCount;


    private void Awake()
    {
        if(autonomous)
            Invoke(nameof(SpawnGroup), initialSpawnDelay);
    }

    private void Update()
    {
        if (!initialSpawnComplete)
            return;
        
        timer += Time.deltaTime;
        if (timer > spawnFrequency)
        {
            SpawnGroup();
            timer = 0f;
        }
    }

    public void SpawnGroup()
    {
        initialSpawnComplete = true;
        
        if (spawnCount <= 0)
            return;
        
        for (int i = groupSize; i > 0; i--)
        {
            SpawnSingleEnemy();
        }

        spawnCount--;
    }

    private void SpawnSingleEnemy()
    {
        Vector3 randPos = Random.insideUnitSphere * Random.Range(0f, spawnRadius);
        randPos.y = verticalPos;
        
        if (!NavMesh.SamplePosition(randPos, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            Debug.LogWarning($"{name} deleted because entity is off nav mesh");
            SpawnSingleEnemy();
            return;
        }
        
        ObjectPoolManager.SpawnObject(enemyToSpawn, transform.position + randPos, enemyToSpawn.transform.rotation,
            ObjectPoolManager.PoolType.GameObject);
    }
}
