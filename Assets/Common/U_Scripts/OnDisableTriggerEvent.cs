using UnityEngine;
using UnityEngine.Events;

public class OnDisableTriggerEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent disableEvent;
    
    private void OnDisable()
    {
        disableEvent.Invoke();
    }
}
