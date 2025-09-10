using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeShopManager : MonoBehaviour
{
    [SerializeField] private TMP_Text upgradeDescriptionText, upgradeCostText;

    private void Start()
    {
        foreach(Button upgradeButton in transform.GetComponentsInChildren<Button>())
        {   
            upgradeButton.onClick.AddListener(delegate
            {
                DisplayUpgradeDetails(upgradeButton.GetComponent<Upgrade>());
            });
        }
    }

    public void DisplayUpgradeDetails(Upgrade upgrade)
    {
        upgradeDescriptionText.text = upgrade.UpgradeDescription;
        upgradeCostText.text = upgrade.Cost.ToString();
    }
}
