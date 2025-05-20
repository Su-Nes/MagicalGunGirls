using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class OffsetMaterialUV : MonoBehaviour
{
    [SerializeField] private Vector2 offsetTransform;
    
    private Material mat;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        mat.mainTextureOffset += offsetTransform * Time.deltaTime;
    }
}
