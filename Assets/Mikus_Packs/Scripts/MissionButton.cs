using UnityEngine;
using UnityEngine.UI;

public class MissionButton : MonoBehaviour
{
    public Mission mission;

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
        MissionSelectManager.Instance.DisplayMissionDetails(mission);
    }
}
