using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;

    private int enemiesToKill;
    private int enemiesKilled;

    private void Awake()
    {
        // Ensure there's only one MissionManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); // keep manager between scenes... just in case
    }

    public void SetEnemyCount(int count)
    {
        enemiesToKill = count;
        enemiesKilled = 0;
    }

    public void EnemyKilled()
    {
        enemiesKilled++;

        if (enemiesKilled >= enemiesToKill)
        {
            Debug.Log("Mission complete! Returning to base camp...");
            SceneManager.LoadScene("BaseCamp"); // Add BaseCamp
        }
    }
}