using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotateScript : MonoBehaviour
{
    public float rotSpeed = 5;
    float t;

    public bool accelerate = false;
    public float acceleration = .001f;

    void FixedUpdate()
    {
        if(accelerate)
            rotSpeed += acceleration;

        t += Time.deltaTime * rotSpeed; 
        RenderSettings.skybox.SetFloat("_Rotation", t);
    }
}
