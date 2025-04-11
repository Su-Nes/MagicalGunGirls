using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField] private Transform targetTf;
    private Transform playerTf;
    
    private void Start()
    {
        playerTf = GameObject.FindWithTag("Player").transform;
    }

    public void Teleport()
    {
        playerTf.position = targetTf.position;
    }
}
