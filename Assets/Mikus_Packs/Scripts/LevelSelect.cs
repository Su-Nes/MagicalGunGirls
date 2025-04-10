using System.Collections;
using System.Collections.Generic;
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

    [Header("Mission 2 Settings")]
    [SerializeField] private Button mission2Button; // Mission 2 button
    [SerializeField] private GameObject mission2Object; // Parent object to activate

    [Header("Objects to Deactivate")]
    [SerializeField] private List<GameObject> objectsToDeactivate; // Objects to disable

    private void Awake()
    {
        if (mission2Button != null)
        {
            mission2Button.onClick.AddListener(ActivateMission2Objects);
        }
    }

    public void SelectLevel()
    {
        StartCoroutine(LevelSelectCoroutine());
    }

    private IEnumerator LevelSelectCoroutine()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelName);
    }

    private void ActivateMission2Objects()
    {
        if (mission2Object != null)
        {
            mission2Object.SetActive(true);

            foreach (Transform child in mission2Object.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    public void DeactivateAllSelectedObjects()
    {
        foreach (GameObject obj in objectsToDeactivate)
        {
            if (obj != null)
            {
                obj.SetActive(false);
                foreach (Transform child in obj.transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }
}
