using System;
using UnityEngine.UI;
using UnityEngine;

public class SetActiveCharacter : MonoBehaviour
{
    [Header("INSERT STATS OBJECT")]
    [SerializeField] private Stats characterStatSO;
    [Header("END STATS OBJECT")]
    
    [SerializeField] private Transform activeComponentParent;
    private Transform characterSelectUI;
    private Image selectUIImage;
    
    private PlayerMovement playerMovement;
    
    [SerializeField] private float UIActiveAlpha, UIInactiveAlpha;
    [SerializeField] private float selectedWidth, unselectedWidth;


    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    public void SetCharacterState(bool activeSelf)
    {
        if(activeSelf)
            playerMovement.MoveSpeed = characterStatSO.moveSpeed;
        
        activeComponentParent.gameObject.SetActive(activeSelf); // activate character components

        if (selectUIImage == null)
        {
            characterSelectUI = GameObject.Find("CharacterSelectLayout").transform.GetChild(transform.GetSiblingIndex());
            selectUIImage = characterSelectUI.GetComponent<Image>();
        }
        
        // change UI
        selectUIImage.rectTransform.sizeDelta = activeSelf
            ? new Vector2(selectedWidth, selectUIImage.rectTransform.sizeDelta.y)
            : new Vector2(unselectedWidth, selectUIImage.rectTransform.sizeDelta.y);
    
        Color panelColor = selectUIImage.color; // set panel alpha according to active state
        selectUIImage.color = activeSelf
            ? new Color(panelColor.r, panelColor.g, panelColor.b, UIActiveAlpha)
            : new Color(panelColor.r, panelColor.g, panelColor.b, UIInactiveAlpha);

        characterSelectUI.GetComponent<HorizontalLayoutGroup>().childAlignment = activeSelf
            ? TextAnchor.MiddleRight
            : TextAnchor.MiddleLeft;

        /*foreach (Transform child in characterSelectUI)
        {
            if(child.GetSiblingIndex() != 0) // first child under UI should always be the character name text object
                child.gameObject.SetActive(!activeSelf); // if character is active, only the name is displayed
        }         */ // if character is inactive, also display ammo and cooldown stats
    }
}
