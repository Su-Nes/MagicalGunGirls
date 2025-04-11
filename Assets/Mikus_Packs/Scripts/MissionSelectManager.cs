using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionSelectManager : MonoBehaviour
{
    public static MissionSelectManager Instance;

    public TMP_Text missionTitleText; //Self explanitory
    public TMP_Text missionDescriptionText; //Same here
    public Button confirmButton; //Here too 

    private string selectedSceneName; //Mmmmmmmmm.... level

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (confirmButton != null)
        {
            confirmButton.onClick.AddListener(OnConfirmMission);
        }
    }

    public void DisplayMissionDetails(string title, string description, string scene)
    {
        missionTitleText.text = title;
        missionDescriptionText.text = description;
        selectedSceneName = scene;
    }

    private void OnConfirmMission()
    {
        if (!string.IsNullOrEmpty(selectedSceneName))
        {
            SceneManager.LoadScene(selectedSceneName);
        }
    }
}