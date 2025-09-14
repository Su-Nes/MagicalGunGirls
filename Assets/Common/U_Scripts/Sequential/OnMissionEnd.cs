using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OnMissionEnd : MonoBehaviour
{
    [SerializeField] private int nectarReward;
    [SerializeField] private string transitionSceneName = "StoryDialogueScene";
    
    public void EndMission()
    {
        DataPersistenceManager.Instance.AddMendingNectar(nectarReward);
        DataPersistenceManager.Instance.missionsCompleted++;
        GameManager.Instance.DestroyAllEnemies();
        GameManager.Instance.LoadSceneWithName(transitionSceneName);
    }
}
