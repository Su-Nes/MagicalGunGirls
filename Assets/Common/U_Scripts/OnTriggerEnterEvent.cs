using UnityEngine.Events;
using UnityEngine;

public class OnTriggerEnterEvent : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";
    [SerializeField] private UnityEvent eventToTrigger;
    [SerializeField] private bool destroyOnTrigger;
    
    private void OnTriggerEnter(Collider other)
    {
        if (targetTag.Length > 0)
        {
            if (other.CompareTag(targetTag))
                eventToTrigger.Invoke();
        }else
            eventToTrigger.Invoke();
        
        if (destroyOnTrigger)
            Destroy(gameObject);
    }
}
