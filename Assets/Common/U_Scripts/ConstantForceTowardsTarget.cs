using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof (Rigidbody))]
public class ConstantForceTowardsTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float force = 1f;
    [SerializeField] private float randomForce = 1f;
    
    private Rigidbody rb;
    
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (target == null)
            target = transform.parent;

        transform.position = target.position;
    }

    private void FixedUpdate()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        rb.AddForce(direction * force + Random.insideUnitSphere * randomForce);
    }
}
