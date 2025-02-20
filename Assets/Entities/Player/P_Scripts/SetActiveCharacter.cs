using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SetActiveCharacter : MonoBehaviour
{
    [SerializeField] private Transform activeComponentParent;
    private Transform characterSelectUI;
    private Image selectUIImage;
    
    [SerializeField] private float UIActiveAlpha, UIInactiveAlpha;
    [SerializeField] private float selectedWidth, unselectedWidth;
    
    private void Start()
    {
        characterSelectUI = GameObject.Find("CharacterSelectLayout").transform.GetChild(transform.GetSiblingIndex());
        selectUIImage = characterSelectUI.GetComponent<Image>();
    }

    public void SetCharacterState(bool activeSelf)
    {
        activeComponentParent.gameObject.SetActive(activeSelf); // activate character components
        if (TryGetComponent(out AttackManager attacks))
        {
            attacks.ReleaseAllManagedAttacks();
        }
        
        // change UI
        if (selectUIImage != null) // this if is just to stop the unassigned error. idk why it shows up but everything works *shrug*
        {
            selectUIImage.rectTransform.sizeDelta = activeSelf
                ? new Vector2(selectedWidth, selectUIImage.rectTransform.sizeDelta.y)
                : new Vector2(unselectedWidth, selectUIImage.rectTransform.sizeDelta.y);
        
            Color panelColor = selectUIImage.color; // set panel alpha according to active state
            selectUIImage.color = activeSelf
                ? new Color(panelColor.r, panelColor.g, panelColor.b, UIActiveAlpha)
                : new Color(panelColor.r, panelColor.g, panelColor.b, UIInactiveAlpha);
            
            foreach (Transform child in characterSelectUI)
            {
                if(child.GetSiblingIndex() != 0) // first child under UI should always be the character name text object
                    child.gameObject.SetActive(!activeSelf); // if character is active, only the name is displayed
            }                                               // if character is inactive, also display ammo and cooldown stats
        }
    }
}
