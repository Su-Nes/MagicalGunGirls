using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class RandomMissionPool : MonoBehaviour
{
    [SerializeField] private GameObject missionButtonPrefab;
    [SerializeField] private Transform missionGroupObject;

    [SerializeField] private int minMissions = 2, maxMissions = 4;
    
    [SerializeField] private Mission TheRottenCoreMission;
    [SerializeField] private List<Mission> missions = new();
    

    public void OnEnable()
    {
        SceneManager.activeSceneChanged += UpdateMissionBoard;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= UpdateMissionBoard;
    }

    public void RemoveMission(Mission mission)
    {
        
    }

    private void UpdateMissionBoard(Scene sceneFrom, Scene sceneTo)
    {
        // destroy old missions
        foreach (Transform child in missionGroupObject)
        {
            Destroy(child.gameObject);
        }

        // generate new missions
        foreach (Mission mission in missions) // instantiate ALL missions
        {
            MissionButton newMission = Instantiate(missionButtonPrefab, Vector3.zero, Quaternion.identity, missionGroupObject).GetComponent<MissionButton>();
            newMission.mission = mission;
            newMission.transform.GetChild(0).GetComponent<TMP_Text>().text = mission.missionTitle;
        }

        // destroy random missions until there's a couple left
        for (int i = missions.Count - Random.Range(minMissions, maxMissions + 1) + 1; i > 0; i--)
        {
            Destroy(missionGroupObject.GetChild(Random.Range(0, missionGroupObject.childCount)).gameObject);
        }
        
        // instantiate the constant mission and moves to the top of the child list
        MissionButton finalMission = Instantiate(missionButtonPrefab, Vector3.zero, Quaternion.identity, missionGroupObject).GetComponent<MissionButton>();
        finalMission.mission = TheRottenCoreMission;
        finalMission.transform.GetChild(0).GetComponent<TMP_Text>().text = TheRottenCoreMission.missionTitle;
        finalMission.transform.SetAsFirstSibling();
    }
}
