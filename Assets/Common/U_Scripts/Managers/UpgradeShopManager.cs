using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeShopManager : MonoBehaviour
{
    [SerializeField] private TMP_Text upgradeDescriptionText, upgradeCostText;
    [SerializeField] private Button purchaseButton;
    [SerializeField] private Stats characterToUpgrade;
    private Upgrade selectedUpgrade;

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

    private void DisplayUpgradeDetails(Upgrade upgrade)
    {
        selectedUpgrade = upgrade;
        
        upgradeDescriptionText.text = upgrade.UpgradeDescription;
        upgradeCostText.text = upgrade.Cost.ToString();
    }

    private void Update()
    {
        if (selectedUpgrade == null)
        {
            purchaseButton.interactable = false;
            return;
        }
        
        purchaseButton.interactable = selectedUpgrade.Cost < DataPersistenceManager.Instance.mendingNectar;
    }

    public void ConfirmUpgrade()
    {
        // todo: disable purchase button if not enough money
        
        characterToUpgrade.ApplyUpgrade(selectedUpgrade);
    }
}
