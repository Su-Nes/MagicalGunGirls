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
            Vector2 randomDirection = Random.insideUnitCircle;
            Vector3 randBasePosition = transform.localPosition + 
                new Vector3(randomDirection.normalized.x, 0f, randomDirection.normalized.y) * (outsideOfCamDistance + groupSpawnRadius);
            
            SpawnObjectsRandomlyInCircleAtPoint(Random.Range(groupSizeMin, groupSizeMax), randBasePosition);
            
            timer = Random.Range(spawnTimerMin, spawnTimerMax);
        }
    }

    private void SpawnObjectsRandomlyInCircleAtPoint(int groupSize, Vector3 centrePoint)
    {
        for (int i = groupSize; i != 0; i--)
        {
            Vector3 randRadiusPosition = transform.position + 
                new Vector3(Random.insideUnitCircle.x, 0f, Random.insideUnitCircle.y) * Random.Range(0f, groupSpawnRadius);

            ObjectPoolManager.SpawnObject(enemyRandomnessList[Random.Range(0, enemyRandomnessList.Length)], 
                centrePoint + randRadiusPosition, Quaternion.identity, ObjectPoolManager.PoolType.GameObject);
        }
    }
}
