using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float playerGravity = 9f;
    [SerializeField] private float additionalMovementDecay = .8f;
    
    private CharacterController controller;
    private Vector3 direction, addedMovement;
    
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        controller.Move(Vector3.down * playerGravity);
        addedMovement *= additionalMovementDecay;
    }

    private void Update()
    {
        direction = new(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        controller.Move(direction.normalized * (moveSpeed * Time.deltaTime) + addedMovement);
    }

    public Vector3 GetVelocity()
    {
        direction = new(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        return direction.normalized * (moveSpeed * Time.deltaTime);
    }

    public void AddMoveVelocity(Vector3 vector)
    {
        addedMovement += vector;
    }
}
