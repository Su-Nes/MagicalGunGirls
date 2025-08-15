using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePauseForScene : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.DisablePauseForCurrentScene();
    }
}
