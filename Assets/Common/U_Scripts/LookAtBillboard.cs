using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtBillboard : MonoBehaviour
{
    private Transform target;
    
    private void Start()
    {
        if (Camera.main != null) target = Camera.main.transform;
    }

    private void Update()
    {
        transform.LookAt(target);
    }
}
