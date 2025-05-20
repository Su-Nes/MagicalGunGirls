using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionSelectManager : MonoBehaviour
{
    public static MissionSelectManager Instance;

    [SerializeField] private TMP_Text missionTitleText, missionDescriptionText, missionUpgradeText;
    [SerializeField] private Button confirmButton;

    private string selectedSceneName;

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

    public void DisplayMissionDetails(string title, string description, string scene, Upgrade upgrade)
    {
        missionTitleText.text = title;
        missionDescriptionText.text = description;
        missionUpgradeText.text = upgrade.upgradeDescription;
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