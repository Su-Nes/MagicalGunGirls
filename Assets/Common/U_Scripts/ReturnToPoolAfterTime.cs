using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPoolAfterTime : MonoBehaviour
{
    [SerializeField] private float time = 1f;
    
    private void OnEnable()
    {
        StartCoroutine(DisableAfterTime());
    }

    private IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(time);
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}
