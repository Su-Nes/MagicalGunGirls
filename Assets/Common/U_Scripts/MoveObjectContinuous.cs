using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectContinuous : MonoBehaviour
{
    public float moveSpeed = 10f;
    
    private void FixedUpdate()
    {
        transform.position += transform.forward * moveSpeed;
    }
}
