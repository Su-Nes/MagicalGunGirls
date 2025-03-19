using UnityEngine;
using Yarn.Unity;

public class SetActiveGameObjectThroughDialogue : MonoBehaviour
{
    [SerializeField] private GameObject objToEnable, objToDisable;
    
    [YarnCommand("EnableGameObject")]
    public void EnableObject()
    {
        objToEnable.SetActive(true);
    }
    
    [YarnCommand("DisableGameObject")]
    public void DisableObject()
    {
        objToDisable.SetActive(false);
    }
}
