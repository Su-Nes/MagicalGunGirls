using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnObjectsOutsideViewport : MonoBehaviour
{
    [SerializeField] private TMP_Text timerDisplay;
    [SerializeField] private GameObject[] enemyRandomnessList;
    //[SerializeField] private float spawnTimerMin, spawnTimerMax;
    [SerializeField] private float startWaveTimer = 15f;
    [SerializeField] private float waveTimerAmplitude = 3f;
    [SerializeField] private float minimumWaveTimer = 5f;
    [SerializeField] private float waveTimerDecrease = .5f;
    private float waveTimer;
    
    private float timer;
    
    [SerializeField] private int groupSizeMin, groupSizeMax;
    
    [SerializeField] private float outsideOfCamDistance = 15f;
    [SerializeField] private float groupSpawnRadius = 5f;

    private void Start()
    {
        waveTimer = startWaveTimer;
        timer = 3f; // quick countdown before the enemies spawn
    }

    private void Update()
    {
        timerDisplay.text = "next\nwave\n" + timer.ToString("F2");
        
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnObjectsRandomlyInCircleAtPoint(Random.Range(groupSizeMin, groupSizeMax), RandomCirclePoint());
            SpawnObjectsRandomlyInCircleAtPoint(Random.Range(groupSizeMin, groupSizeMax), RandomCirclePoint());
            
            timer = waveTimer + Random.Range(-waveTimerAmplitude, waveTimerAmplitude);
            waveTimer -= waveTimerDecrease;
            if (waveTimer < minimumWaveTimer)
                waveTimer = minimumWaveTimer;
        }
    }

    private Vector3 RandomCirclePoint()
    {
        return transform.localPosition + 
               new Vector3(Random.insideUnitCircle.normalized.x, 0f, Random.insideUnitCircle.normalized.y) * (outsideOfCamDistance + groupSpawnRadius);
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
