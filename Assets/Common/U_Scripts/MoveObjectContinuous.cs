using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectContinuous : MonoBehaviour
{
    [Tooltip("Lifetime of this object. If 0, lives forever.")] 
    [SerializeField] private float lifetime = 2.5f;
    public float moveSpeed = 10f;
    

    private void OnEnable()
    {
        if (lifetime > 0f)
            StartCoroutine(ReturnToPoolAfterTime());
        
        if (moveSpeed < 0)
            ReverseMoveDirection();
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * moveSpeed;
    }

    public void ReverseMoveDirection()
    {
        moveSpeed *= -1f;
    }
    
    private IEnumerator ReturnToPoolAfterTime()
    {
        float timer = 0f;
        while (timer < lifetime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}