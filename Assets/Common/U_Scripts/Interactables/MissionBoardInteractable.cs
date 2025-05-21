using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionBoardInteractable : Interactable
{
    public override void OnInteract()
    {
        MissionSelectManager.Instance.ToggleDisplay();
    }
}
