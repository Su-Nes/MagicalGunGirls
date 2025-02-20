using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public float playerGravity = 9f;
    
    public float moveSpeed = 10f;
    private Vector3 direction;
    
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        controller.Move(Vector3.down * playerGravity);
    }

    private void Update()
    {
        direction = new(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        controller.Move(direction.normalized * (moveSpeed * Time.deltaTime));
    }

    public Vector3 GetVelocity()
    {
        direction = new(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        return direction.normalized * (moveSpeed * Time.deltaTime);
    }
}
