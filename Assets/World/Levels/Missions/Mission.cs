using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mission")]
public class Mission : ScriptableObject
{
    public string missionTitle; // Self explanatory
    [TextArea(2, 5)]
    public string description; // Same here
    public string sceneToLoad; // Here too
    public Upgrade upgrade;
}
