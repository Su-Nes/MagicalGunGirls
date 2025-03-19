using System.Collections;
using UnityEngine;

public class MoveObjectContinuous : MonoBehaviour
{
    [Tooltip("Lifetime of this object. If 0, lives forever.")] 
    [SerializeField] private float lifetime = 2.5f;
    public float moveSpeed = 10f;

    private Vector3 direction;
    

    private void OnEnable()
    {
        if (lifetime > 0f)
            StartCoroutine(ReturnToPoolAfterTime());

        direction = transform.forward;
    }

    private void FixedUpdate()
    {
        transform.position += direction.normalized * moveSpeed;
    }

    public void SetMovementDirection(Vector3 dir)
    {
        direction = dir;
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