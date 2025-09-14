using UnityEngine.Events;
using UnityEngine;

public class OnTriggerEnterEvent : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";
    [SerializeField] private UnityEvent eventToTrigger;
    [SerializeField] private bool printLogOnTrigger, destroyOnTrigger;
    private bool onlyCallCompleted;
    
    private void OnTriggerEnter(Collider other)
    {
        if (onlyCallCompleted)
            return;
        
        if (targetTag.Length > 0)
        {
            if (other.CompareTag(targetTag))
                InvokeEvent();
        }
        else
            InvokeEvent();
    }

    private void InvokeEvent()
    {
        eventToTrigger.Invoke();
        
        if (printLogOnTrigger)
            Debug.Log($"Event invoked on {name}.");
        
        if (destroyOnTrigger)
        {
            onlyCallCompleted = true;
            
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
        
    }
}
