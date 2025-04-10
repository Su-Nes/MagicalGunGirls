using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionSelectManager : MonoBehaviour
{
    public static MissionSelectManager Instance;

    public Text missionNameText; //Self explanitory
    public Text missionDescriptionText; //Same here
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

    public void DisplayMissionDetails(string name, string description, string scene)
    {
        missionNameText.text = name;
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