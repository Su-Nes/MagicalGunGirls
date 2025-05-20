using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AddForceOnStart : MonoBehaviour
{
    [SerializeField] private Vector3 modAngle;
    [SerializeField] private float forceMult = 5f;
    
    private void Start()
    {
        GetComponent<Rigidbody>().AddForce((transform.forward + modAngle) * forceMult, ForceMode.Impulse);
    }
}
