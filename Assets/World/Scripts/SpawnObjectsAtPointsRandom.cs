using UnityEngine;
using Yarn.Unity;

public class SpawnObjectsAtPointsRandom : MonoBehaviour
{
    public bool isActive = true;
    
    [SerializeField] private GameObject[] randomEnemyPool;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnTimerMin, spawnTimerMax;
    private float timer;
    
    
    private void Update()
    {
        if (!isActive)
            return;
        
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnEnemiesAtPoints();
            
            timer = Random.Range(spawnTimerMin, spawnTimerMax);
        }
    }

    private void SpawnEnemiesAtPoints()
    {
        foreach (Transform point in spawnPoints)
        {
            int randEnemyIndex = Random.Range(0, randomEnemyPool.Length);
            ObjectPoolManager.SpawnObject(randomEnemyPool[randEnemyIndex], point.position, randomEnemyPool[randEnemyIndex].transform.rotation, ObjectPoolManager.PoolType.GameObject);
        }
    }

    [YarnCommand("SetSpawnerActivity")]
    public void SetSpawnerActivity(bool state)
    {
        isActive = state;
    }
}
