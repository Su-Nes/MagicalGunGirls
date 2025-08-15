using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallLoadScene : MonoBehaviour
{
    public void LoadNextScene()
    {
        GameManager.Instance.LoadNextSceneInBuild();
    }

    public void LoadSceneWithName(string scene)
    {
        GameManager.Instance.LoadSceneWithName(scene);
    }
}
