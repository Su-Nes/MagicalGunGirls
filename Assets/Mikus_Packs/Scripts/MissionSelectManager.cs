using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionSelectManager : MonoBehaviour
{
    public static MissionSelectManager Instance;

    [SerializeField] private GameObject missionCanvas;
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

    public void ToggleDisplay()
    {
        missionCanvas.SetActive(!missionCanvas.activeSelf);
        GameManager.Instance.FreezePlayer = missionCanvas.activeSelf;
    }

    public void DisplayMissionDetails(Mission mission)
    {
        missionTitleText.text = mission.missionTitle;
        missionDescriptionText.text = mission.description;
        missionUpgradeText.text = $"Reward: {mission.upgrade.upgradeDescription}";
        selectedSceneName = mission.sceneToLoad;
    }

    private void OnConfirmMission()
    {
        if (!string.IsNullOrEmpty(selectedSceneName))
        {
            SceneManager.LoadScene(selectedSceneName);
        }
    }
}