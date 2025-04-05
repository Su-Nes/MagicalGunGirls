using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelectionTrigger : MonoBehaviour
{
    [SerializeField] private GameObject levelSelect;  // Just the object to select levels
    [SerializeField] private TextMeshProUGUI levelText; // Text goes here (blah, blah, blah)
    [SerializeField] private Button selectButton; // Button to start the level
    [SerializeField] private string levelName; // Add in inspector whichever fucking level is needed

    public void SelectLevel()
    {
        StartCoroutine(LevelSelectCoroutine());
    }

    private IEnumerator LevelSelectCoroutine()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelName);
    }
}
