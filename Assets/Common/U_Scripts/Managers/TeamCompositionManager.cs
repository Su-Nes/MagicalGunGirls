using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamCompositionManager : MonoBehaviour
{
    [SerializeField] private Transform viewport;
    [SerializeField] private float contentHeightPerCharacter = 106f;

    private void Start()
    {
        Time.timeScale = 0f;

        RectTransform contentTransform = viewport.GetComponent<RectTransform>();
        contentTransform.sizeDelta = new Vector2(contentTransform.sizeDelta.x, 0f);
        foreach (GameObject character in DataPersistenceManager.Instance.UnlockedCharacters)
        {
            Instantiate(character.GetComponent<CharacterUIManager>().descriptionUIObj, Vector3.zero, Quaternion.identity, viewport);
            contentTransform.sizeDelta += new Vector2(0f, contentHeightPerCharacter);
        }
    }

    public void CloseTeamScreen()
    {
        if (FindObjectOfType<CharacterSwap>().CharacterCount > 0)
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
    }
}
