using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class ReadAmmoCount : MonoBehaviour
{
    public AmmoManager _AmmoManager;
    private TMP_Text ammoText;

    private void Start()
    {
        ammoText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (_AmmoManager != null)
            ammoText.text = _AmmoManager.AmmoString();
    }
}
