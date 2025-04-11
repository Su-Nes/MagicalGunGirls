using UnityEngine;
using UnityEngine.UI;

public class MissionButton : MonoBehaviour
{
    [SerializeField] private string missionName; // Self explanatory
    [TextArea(2, 5)]
    [SerializeField] private string missionDescription; // Same here
    [SerializeField] private string sceneToLoad; // Here too

    private Button button; // Add the button that's needed

    private void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnMissionSelected);
        }
    }

    private void OnMissionSelected()
    {
        MissionSelectManager.Instance.DisplayMissionDetails(missionName, missionDescription, sceneToLoad);
    }
}
