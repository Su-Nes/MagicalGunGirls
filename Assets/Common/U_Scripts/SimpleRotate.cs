using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;
    
    private void Update()
    {
        transform.Rotate(rotation * Time.deltaTime);
    }
}
