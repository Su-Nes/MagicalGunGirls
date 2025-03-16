using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelectionTrigger : MonoBehaviour
{
    [SerializeField] private GameObject panel;  // I had no idea how to make it, but i assume it uses UI Panels
    [SerializeField] private TextMeshProUGUI levelText; // Text goes here (blah, blah, blah)
    [SerializeField] private Button selectButton; // Button to start the level
    [SerializeField] private string levelName; // Add in inspector whichever fucking level is needed

    private void Start()
    {
        if (panel != null)
            panel.SetActive(false); // Hide panel initially
    }

    private void OnMouseDown()
    {
        if (panel != null && levelText != null && selectButton != null)
        {
            panel.SetActive(true);
            levelText.text = "Do you want to enter " + levelName + "?";

            selectButton.onClick.RemoveAllListeners(); // Clear previous listeners
            selectButton.onClick.AddListener(() => LoadLevel());
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelName);
    }
}
