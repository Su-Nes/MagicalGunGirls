using System.Collections;
using UnityEngine;

public class ReleaseAfterTime : MonoBehaviour
{
    [SerializeField] private float lifetime;
    
    private void OnEnable()
    {
        StartCoroutine(ReturnToPoolAfterTime());
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
