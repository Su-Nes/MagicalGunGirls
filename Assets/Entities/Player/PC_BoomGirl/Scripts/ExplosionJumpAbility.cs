using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionJumpAbility : RepelAbility
{
    [Header("Explo jumpo:")]
    [SerializeField] private float verticalVelocity = 10f;
    
    private PlayerMovement _playerController;


    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerMovement>();
    }

    protected override void ActivateAbility()
    {
        base.ActivateAbility();

        _playerController.AddMoveVelocity(new Vector3(0f, verticalVelocity, 0f));
    }
}
