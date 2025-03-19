using UnityEngine.Events;
using UnityEngine;

public class SequentialEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent[] events;
    private int index;
    
    
    public void TriggerEvent()
    {
        events[index].Invoke();
        
        index++;
        if (index > events.Length - 1)
            index = 0;
    }
}
