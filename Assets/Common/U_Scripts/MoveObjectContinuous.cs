using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MoveObjectContinuous : MonoBehaviour
{
    [Tooltip("Lifetime of this object. If 0, lives forever.")] 
    [SerializeField] private float lifetime = 5f;
    public float moveSpeed = 10f;

    private enum StartDirection
    {
        Forward,
        Backward,
        Left,
        Right,
        Up,
        Down
    }
    [SerializeField] private StartDirection startDirection;
    private Vector3 direction;
    

    private void OnEnable()
    {
        if (lifetime > 0f)
            StartCoroutine(ReturnToPoolAfterTime());

        direction = startDirection switch
        {
            StartDirection.Forward => transform.forward,
            StartDirection.Backward => -transform.forward,
            StartDirection.Left => -transform.right,
            StartDirection.Right => transform.right,
            StartDirection.Up => transform.up,
            StartDirection.Down => -transform.up,
            _ => transform.forward
        };
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