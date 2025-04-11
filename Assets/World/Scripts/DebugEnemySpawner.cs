using UnityEngine;

public class DebugEnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy1, enemy2, enemy3;
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

    public void SpawnGroup()
    {
        for(int i = groupSize; i != 0; i--)
        {
            Vector3 randPosition = new(Random.Range(-spawnRadius, spawnRadius), 0f,
                Random.Range(-spawnRadius, spawnRadius));
            ObjectPoolManager.SpawnObject(enemy1, transform.position + randPosition, transform.rotation, transform);
            ObjectPoolManager.SpawnObject(enemy2, transform.position + randPosition, transform.rotation, transform);
            ObjectPoolManager.SpawnObject(enemy3, transform.position + randPosition, transform.rotation, transform);
        }
    }
}
