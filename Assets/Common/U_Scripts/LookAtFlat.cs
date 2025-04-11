using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtFlat : MonoBehaviour
{
    [SerializeField] private Transform targetTf;
    private Vector3 targetFlatPos;
    
    
    private void Update()
    {
        targetFlatPos = targetTf.position;
        targetFlatPos.y = transform.position.y;
        transform.LookAt(targetFlatPos);
    }
}
