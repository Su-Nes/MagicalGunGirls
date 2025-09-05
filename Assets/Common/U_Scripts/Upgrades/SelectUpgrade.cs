using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Upgrade))]
public class SelectUpgrade : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(SelectUpgradeToBuy);
    }

    private void SelectUpgradeToBuy()
    {
        BarracksUpgradeManager.Instance.DisplayUpgradeDetails(GetComponent<Upgrade>());
    }
}
