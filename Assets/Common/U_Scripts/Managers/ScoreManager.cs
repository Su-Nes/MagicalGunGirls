using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance { get { return _instance; } }

    [SerializeField] private TMP_Text scoreDisplay;
    private int score;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
    }
    

    private void Start()
    {
        scoreDisplay.text = GetScoreString();
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        scoreDisplay.text = GetScoreString();
    }

    public int GetScore()
    {
        return score;
    }

    private string GetScoreString()
    {
        return score.ToString("D3") + "\nenemies\nkilled";
    }
}
