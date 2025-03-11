using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    [Header("Start objects: ")] 
    [SerializeField] private GameObject startObjects;
    [SerializeField] private TMP_Text highScoreText;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
        
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString("D3") + " kill record";

        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        startObjects.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        if (PlayerPrefs.GetInt("HighScore") < ScoreManager.Instance.GetScore())
            PlayerPrefs.SetInt("HighScore", ScoreManager.Instance.GetScore());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
