using TMPro;
using UnityEngine;

public class CharacterUIManager : MonoBehaviour
{
    [SerializeField] private string characterName = "MagGirl";
    public string CharacterName { get { return characterName; } }
    [SerializeField] private GameObject nameTextUIObj, ammoUIObj, cooldownUIObj;
    public GameObject descriptionUIObj, upgradeUIObj;
    
    private Transform characterUI;
    
    
    private void Awake() // instantiate all weapon and ability UI objects and assign their managers
    {
        characterUI = GameObject.Find("CharacterSelectLayout").transform.GetChild(transform.GetSiblingIndex());

        GameObject nameObj = Instantiate(nameTextUIObj, Vector3.zero, Quaternion.identity, characterUI);
        nameObj.GetComponent<TMP_Text>().text = characterName;
    }

    public void CreateUI(AmmoManager a_m)
    {
        GameObject newManager = Instantiate(ammoUIObj, Vector3.zero, Quaternion.identity, characterUI);
        newManager.GetComponent<ReadAmmoCount>()._AmmoManager = a_m;
    }

    public void CreateUI(CooldownManager c_m)
    {
        GameObject newManager = Instantiate(cooldownUIObj, Vector3.zero, Quaternion.identity, characterUI);
        newManager.GetComponent<ReadChargeAmount>()._CooldownManager = c_m;
    }
}
