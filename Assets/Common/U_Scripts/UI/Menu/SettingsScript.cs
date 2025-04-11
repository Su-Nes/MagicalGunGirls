using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SettingsScript : MonoBehaviour
{
    public bool enableKeyboard = true;
    public float ExitCheckTime = 3f;

    private Resolution[] resolutions;

    [Header("Assigns: ")]
    [SerializeField] private Toggle toggle;
    [SerializeField] private GameObject startPanel, settingsPanel, exitText;
    [SerializeField] private TMP_Dropdown resDropdown;

    private void Awake()
    {
        resolutions = Screen.resolutions;

        resDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width &&
               resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }

        resDropdown.AddOptions(options);
        resDropdown.value = currentResIndex;
        resDropdown.RefreshShownValue();

        toggle.isOn = Screen.fullScreen;
    }

    private void Start()
    {
        GameManager.Instance.DisablePauseForCurrentScene();
    }

    public void ToggleSettings()
    {
        if (settingsPanel == null)
            return;
        settingsPanel.SetActive(!settingsPanel.activeSelf);
        
        if (startPanel == null)
            return;
        startPanel.SetActive(!startPanel.activeSelf);
    }

    private void Update()
    {
        //controls
        if (enableKeyboard && Input.GetKeyDown(KeyCode.Escape))
            ToggleSettings();

        if (Input.GetKeyDown(KeyCode.F10)) //debug delete player prefs
            PlayerPrefs.DeleteAll();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        if(isFullscreen)
            PlayerPrefs.SetInt("fullscreen", 1);
        else
            PlayerPrefs.SetInt("fullscreen", 0);
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        PlayerPrefs.SetInt("resolution", resIndex);
    }
    

    public void ExitButtonClick()
    {
        if (exitText.activeSelf)
        {
            print("Quit game.");
            Application.Quit();
        }
        else
            StartCoroutine(ExitTime());
    }

    private IEnumerator ExitTime()
    {
        exitText.SetActive(true);
        yield return new WaitForSecondsRealtime(ExitCheckTime);
        exitText.SetActive(false);
    }
}
