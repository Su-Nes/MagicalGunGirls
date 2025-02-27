using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectsOutsideViewport : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyRandomnessList;
    [SerializeField] private float spawnTimerMin, spawnTimerMax;
    private float timer;
    [SerializeField] private int groupSizeMin, groupSizeMax;
    
    [SerializeField] private float outsideOfCamDistance = 15f;
    [SerializeField] private float groupSpawnRadius = 5f;
    
    
    private void Start()
    {
        timer = Random.Range(spawnTimerMin, spawnTimerMax);
    }
    
    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnRandomObjectGroup(Random.Range(groupSizeMin, groupSizeMax));
            
            timer = Random.Range(spawnTimerMin, spawnTimerMax);
        }
    }

    private void SpawnRandomObjectGroup(int groupSize)
    {
        for (int i = groupSize; i != 0; i--)
        {
            Vector3 randBasePosition = transform.position + 
                new Vector3(Random.insideUnitCircle.x, 0f, Random.insideUnitCircle.y) * (outsideOfCamDistance + groupSpawnRadius);
            
            Vector3 randRadiusPosition = transform.position + 
                new Vector3(Random.insideUnitCircle.x, 0f, Random.insideUnitCircle.y) * Random.Range(0f, groupSpawnRadius);

            ObjectPoolManager.SpawnObject(enemyRandomnessList[Random.Range(0, enemyRandomnessList.Length)], 
                randBasePosition + randRadiusPosition, Quaternion.identity, ObjectPoolManager.PoolType.GameObject);
        }
    }
}
