using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BarracksUpgradeManager : MonoBehaviour
{
    public static BarracksUpgradeManager Instance;

    [SerializeField] private GameObject shopCanvas, upgradeSelectCanvas;
    [SerializeField] private Transform characterButtonHolder;
    [SerializeField] private TMP_Text upgradeDescriptionText, upgradeCostText;

    private Upgrade currentUpgrade;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleDisplay()
    {
        shopCanvas.SetActive(!shopCanvas.activeSelf);
        GameManager.Instance.FreezePlayer = shopCanvas.activeSelf;
    }

    public void DisplayUpgradeDetails(Upgrade upgrade)
    {
        currentUpgrade = upgrade;
        upgradeDescriptionText.text = upgrade.upgradeDescription;
        upgradeCostText.text = upgrade.cost.ToString();
    }

    public void OnConfirmUpgrade()
    {
        RefreshCharacterSelect();
        
        upgradeSelectCanvas.SetActive(true);
        foreach (ApplyUpgradeToCharacter character in characterButtonHolder
                     .GetComponentsInChildren<ApplyUpgradeToCharacter>())
        {
            character.upgradeToApply = currentUpgrade;
        }
    }

    private void RefreshCharacterSelect()
    {
        foreach (Transform child in characterButtonHolder)
        {
            Destroy(child.gameObject);
        }

        foreach (GameObject character in DataPersistenceManager.Instance.UnlockedCharacters)
        {
            Instantiate(character.GetComponent<CharacterStatManager>().upgradeUIObj, Vector3.zero, Quaternion.identity,
                characterButtonHolder);
        }
    }

    public void FinishUpgrade()
    {
        upgradeSelectCanvas.SetActive(false);
    }
}
