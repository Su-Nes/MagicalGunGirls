using UnityEngine;

public class SpawnTracker : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount = 10;
    public Transform[] spawnPoints;

    private void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        MissionManager.Instance.SetEnemyCount(enemyCount);

        for (int i = 0; i < enemyCount; i++)
        {
            Transform spawnPoint = spawnPoints[i % spawnPoints.Length];
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}