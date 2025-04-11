using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionBoardInteractable : Interactable
{
    [SerializeField] private GameObject boardCanvas;

    public override void OnInteract()
    {
        boardCanvas.SetActive(!boardCanvas.activeSelf);
    }
}
