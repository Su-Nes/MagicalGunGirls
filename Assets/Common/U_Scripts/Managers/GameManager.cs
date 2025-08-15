using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    
    [SerializeField] private GameObject pauseScreen, dialogueAdvanceButton;
    private bool pauseScreenEnabled;
    [SerializeField] private Image overlay;
    
    private DialogueRunner dialogueRunner;
    public bool FreezePlayer, FreezeEnemies;
    
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

        dialogueRunner = FindObjectOfType<DialogueRunner>();
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
        dialogueRunner.Stop();
        
        postProcessing.profile.TryGet(out ChromaticAberration ca); // reset post processing
        ca.intensity.value = chromeAbbDefault;
        postProcessing.profile.TryGet(out Vignette v);
        v.intensity.value = vignetteDefault;
        
        overlay.color = new Color(Color.black.r, Color.black.g, Color.black.b, 1f);
        overlay.CrossFadeAlpha(0f, .5f, true);

        pauseScreenEnabled = true;
    }
    
    public void GameOver()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    private void Update()
    { // this bool is checked by other scripts. when it is true, their update methods return null
        if (Input.GetButtonDown("Cancel") && pauseScreenEnabled) // toggle pause
        {
            ToggleGamePause();
        }
    }

    public void SetFreezePlayer(bool freeze)
    {
        FreezePlayer = freeze;
    }

    [YarnCommand("FreezePlayer")]
    public void DelayFreezePlayer(bool freeze, float delay = .01f)
    { // this bool is checked by other scripts (player movement mostly). when it is true, their update methods return null
        StartCoroutine(ReheatPlayer(freeze, delay));
    }

    [YarnCommand("AutomaticDialogue")]
    public void SetDialogueAutomatic(bool state, float holdTime = 3f)
    {
        LineView lineView = FindObjectOfType<LineView>();
        lineView.autoAdvance = state;
        lineView.holdTime = holdTime;
        dialogueAdvanceButton.GetComponent<Image>().enabled = !state;
        dialogueAdvanceButton.transform.GetChild(0).gameObject.SetActive(!state);
    }

    private IEnumerator ReheatPlayer(bool freeze, float delay) // this is to bypass line 88
    {
        yield return new WaitForSeconds(delay);
        FreezePlayer = freeze;
    }

    public void ToggleGamePause()
    {
        pauseScreen.SetActive(!pauseScreen.activeSelf);
        Time.timeScale = pauseScreen.activeSelf ? 0f : 1f; // freeze time on pause
    }

    public void DisablePauseForCurrentScene()
    {
        pauseScreenEnabled = false;
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


    [YarnCommand("LoadNextScene")]
    public void LoadNextSceneInBuild(float crossFadeTime = .5f)
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }
    
    [YarnCommand("LoadSceneWithName")]
    public void LoadSceneWithName(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    
    private IEnumerator LoadScene(int sceneIndex, float crossFadeTime = .5f)
    {
        overlay.CrossFadeAlpha(1, crossFadeTime, true);
        
        if (dialogueRunner != null)
            dialogueRunner.Stop();
        yield return new WaitForSeconds(crossFadeTime);
        SceneManager.LoadScene(sceneIndex);
    }
    
    private IEnumerator LoadScene(string sceneName, float crossFadeTime = .5f)
    {
        overlay.CrossFadeAlpha(1, crossFadeTime, true);
        
        if (dialogueRunner != null)
            dialogueRunner.Stop();
        yield return new WaitForSeconds(crossFadeTime);
        SceneManager.LoadScene(sceneName);
    }
}
