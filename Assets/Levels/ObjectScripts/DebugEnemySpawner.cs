using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class DebugEnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float spawnTimer = 3f;
    private float timer;
    [SerializeField] private int groupSize = 6;
    [SerializeField] private float spawnRadius = 6f;
    
    
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnTimer)
        {
            SpawnGroup();
            timer = 0f;
        }
    }

    private void SpawnGroup()
    {
        for(int i = groupSize; i != 0; i--)
        {
            Vector3 randPosition = new(Random.Range(-spawnRadius, spawnRadius), 0f,
                Random.Range(-spawnRadius, spawnRadius));
            Instantiate(enemy, transform.position + randPosition, transform.rotation);
        }
    }
}
