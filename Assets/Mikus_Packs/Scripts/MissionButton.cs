using UnityEngine;
using UnityEngine.UI;

public class MissionButton : MonoBehaviour
{
    public string missionName; // Self explanitory
    public string missionDescription; // Same here
    public string sceneToLoad; // Here too

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
