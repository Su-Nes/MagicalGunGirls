using TMPro;
using UnityEngine;

public class CharacterStatManager : MonoBehaviour
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
        
        foreach (AmmoManager a_m in GetComponents<AmmoManager>())
        {
            GameObject newManager = Instantiate(ammoUIObj, Vector3.zero, Quaternion.identity, characterUI);
            newManager.GetComponent<ReadAmmoCount>()._AmmoManager = a_m; // read script that takes ammo value from assigned manager
        }
        
        foreach (CooldownManager c_m in GetComponents<CooldownManager>())
        {
            GameObject newManager = Instantiate(cooldownUIObj, Vector3.zero, Quaternion.identity, characterUI);
            newManager.GetComponent<ReadChargeAmount>()._CooldownManager = c_m;
        }
    }
}
