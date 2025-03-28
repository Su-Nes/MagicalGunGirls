using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtensionMethods : MonoBehaviour
{
    private static ExtensionMethods _instance;
    public static ExtensionMethods Instance { get { return _instance; } }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
    }

    public static float Remap (float value, float from1, float to1, float from2, float to2) 
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
