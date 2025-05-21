using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class RandomMissionPool : MonoBehaviour
{
    [SerializeField] private GameObject missionButtonPrefab;
    [SerializeField] private Transform missionGroupObject;

    [SerializeField] private int minMissions = 2, maxMissions = 4;
    
    [SerializeField] private Mission TheRottenCoreMission;
    [SerializeField] private Mission[] missions;
    

    public void OnEnable()
    {
        SceneManager.activeSceneChanged += UpdateMissionBoard;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= UpdateMissionBoard;
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
        }

        // destroy random missions until there's a couple left
        for (int i = missions.Length - Random.Range(minMissions, maxMissions + 1) + 1; i > 0; i--)
        {
            print(missionGroupObject.GetChild(Random.Range(0, missionGroupObject.childCount)).gameObject);
            Destroy(missionGroupObject.GetChild(Random.Range(0, missionGroupObject.childCount)).gameObject);
        }
        
        // instantiate the constant mission and moves to the top of the child list
        MissionButton finalMission = Instantiate(missionButtonPrefab, Vector3.zero, Quaternion.identity, missionGroupObject).GetComponent<MissionButton>();
        finalMission.mission = TheRottenCoreMission;
        finalMission.transform.SetAsFirstSibling();
    }
}
