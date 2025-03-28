using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    [SerializeField] private GameObject pauseScreen;
    
    private DialogueRunner dialogueRunner;
    private bool freezePlayer;
    public bool FreezePlayer
    {
        get { return freezePlayer; }
    }
    
    private Volume postProcessing;
    private float chromeAbbDefault, vignetteDefault;
    

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

        postProcessing = transform.GetComponentInChildren<Volume>(); // for resetting post processing
        postProcessing.profile.TryGet(out ChromaticAberration ca);
        chromeAbbDefault = ca.intensity.value;
        postProcessing.profile.TryGet(out Vignette v);
        vignetteDefault = v.intensity.value;
    }
        
    private void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
        // this function gets called whenever a scene in loaded in
    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>(); // get the newly loaded scene's dialogue runner
        
        postProcessing.profile.TryGet(out ChromaticAberration ca); // reset post processing
        ca.intensity.value = chromeAbbDefault;
        postProcessing.profile.TryGet(out Vignette v);
        v.intensity.value = vignetteDefault;
        
        // [could have code for image fade out here]
    }
    
    public void GameOver()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    private void Update()
    { // this bool is checked by other scripts. when it is true, their update methods return null
        freezePlayer = dialogueRunner.IsDialogueRunning || pauseScreen.activeSelf;

        if (Input.GetButtonDown("Cancel")) // toggle pause
        {
            ToggleGamePause();
        }
    }

    public void ToggleGamePause()
    {
        pauseScreen.SetActive(!pauseScreen.activeSelf);
    }

    public void QuitApplication()
    {
        Application.Quit();
        print("Quitting game.");
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
