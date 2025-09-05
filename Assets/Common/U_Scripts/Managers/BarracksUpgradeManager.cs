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
    [SerializeField] private Transform characterButtonHolder, characterStoreHolder;
    [SerializeField] private TMP_Text upgradeDescriptionText, upgradeCostText;
    

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
        
        RefreshCharacterSelect();
    }

    public void DisplayUpgradeDetails(Upgrade upgrade)
    {
        upgradeDescriptionText.text = upgrade.UpgradeDescription;
        upgradeCostText.text = upgrade.Cost.ToString();
    }

    public void OnConfirmUpgrade()
    {
        RefreshCharacterSelect();
        
        upgradeSelectCanvas.SetActive(true);
    }

    private void RefreshCharacterSelect()
    {
        foreach (Transform child in characterButtonHolder)
        {
            Destroy(child.gameObject);
        }

        foreach (GameObject character in DataPersistenceManager.Instance.UnlockedCharacters)
        {
            // create button and assign the menu for it
            GameObject newCharButton = Instantiate(character.GetComponent<CharacterUIManager>().upgradeUIObj, Vector3.zero, Quaternion.identity,
                characterButtonHolder);
            newCharButton.GetComponent<Button>().onClick.AddListener(delegate
                {EnableCharacterUpgradeScreen(character.GetComponent<CharacterUIManager>().CharacterName);});
        }
    }

    public void EnableCharacterUpgradeScreen(string charName)
    {
        int i = 0;
        foreach (Transform button in characterStoreHolder)
        {
            if (button.name.Contains(charName))
                break;
            i++;
        }

        if (i > characterButtonHolder.childCount - 1)
        {
            Debug.LogError($"No corresponding button for name: {charName}");
            return;
        }
        
        characterStoreHolder.GetChild(i).gameObject.SetActive(true);
    }

    public void DisableCharacterUpgradeScreens()
    {
        foreach (Transform button in characterButtonHolder)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void FinishUpgrade()
    {
        upgradeSelectCanvas.SetActive(false);
    }
}
