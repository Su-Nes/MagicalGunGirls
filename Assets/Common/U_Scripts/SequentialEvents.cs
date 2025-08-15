using UnityEngine.Events;
using UnityEngine;
using Yarn.Unity;

public class SequentialEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent[] events;
    private int index;
    
    [YarnCommand("InvokeEvent")]
    public void TriggerEvent()
    {
        events[index].Invoke();
        
        index++;
        if (index > events.Length - 1)
            index = 0;
    }
}
