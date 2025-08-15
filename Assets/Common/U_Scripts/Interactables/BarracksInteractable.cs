using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarracksInteractable : Interactable
{
    public override void OnInteract()
    {
        BarracksUpgradeManager.Instance.ToggleDisplay();
    }
}
