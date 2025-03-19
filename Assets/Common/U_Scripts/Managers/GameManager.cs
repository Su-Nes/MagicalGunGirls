using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    
    private DialogueRunner dialogueRunner;
    private bool freezePlayer;
    public bool FreezePlayer
    {
        get { return freezePlayer; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
        
        DontDestroyOnLoad(gameObject);
    }
    
    
    private void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
        
    private void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
        // this function gets called whenever a scene in loaded in
    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Level Loaded");
        Debug.Log(scene.name);
        Debug.Log(mode);

        dialogueRunner = FindObjectOfType<DialogueRunner>(); // get the newly loaded scene's dialogue runner
        
        // [could have code for image fade out here]
    }

    
    public void GameOver()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    private void Update()
    { // remove all player control and pause enemy AI when dialogue is active
        freezePlayer = dialogueRunner.IsDialogueRunning;
    }
    
    public void Play()
    {
        Time.timeScale = 1f;
    }
    
    public void Pause()
    {
        Time.timeScale = 0f;
    }

    [YarnCommand("DestroyAllEnemies")]
    public void DestroyAllEnemies()
    {
        EnemyAI[] enemies = FindObjectsOfType<EnemyAI>();
        foreach (EnemyAI enemy in enemies)
        {
            ObjectPoolManager.ReturnObjectToPool(enemy.gameObject);
        }
    }

    // this could be made into an IEnumerator for the fade
    [YarnCommand("LoadNextScene")]
    public void LoadNextSceneInBuild()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    
    public IEnumerator LoadScene(int sceneIndex)
    {
        // [insert screen fade code here]
        
        dialogueRunner.Stop();
        yield return null;
        SceneManager.LoadScene(sceneIndex);
    }
}
