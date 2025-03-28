using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimationController : MonoBehaviour
{
    private Animator animator;
    private Vector3 moveVector;

    [SerializeField] private string primaryAttackName, secondaryAttackName;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        moveVector = new(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        if (Input.GetButtonDown("Fire1"))
        {
            animator.Play(primaryAttackName);
        }
        
        if (Input.GetButtonDown("Fire2"))
        {
            animator.Play(secondaryAttackName);
        }
    }
}
