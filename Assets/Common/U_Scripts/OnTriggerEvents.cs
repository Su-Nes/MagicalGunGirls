using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class OnTriggerEvents : MonoBehaviour
{
    [SerializeField] private string targetTag;
    [SerializeField] private UnityEvent onTriggerEnter, onTriggerStay, onTriggerExit;
    
    private void OnTriggerEnter(Collider other)
    {
        if (targetTag.Length == 0)
            onTriggerEnter.Invoke();
        else if (other.CompareTag(targetTag))
            onTriggerEnter.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        if (targetTag.Length == 0)
            onTriggerStay.Invoke();
        else if (other.CompareTag(targetTag))
            onTriggerStay.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (targetTag.Length == 0)
            onTriggerExit.Invoke();
        else if (other.CompareTag(targetTag))
            onTriggerExit.Invoke();
    }
}
