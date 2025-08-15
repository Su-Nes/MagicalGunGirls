using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectUpgrade : MonoBehaviour
{
    [SerializeField] private Upgrade upgrade;
    
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(SelectUpgradeToBuy);
    }

    private void SelectUpgradeToBuy()
    {
        BarracksUpgradeManager.Instance.DisplayUpgradeDetails(upgrade);
    }
}
