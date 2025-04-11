using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    private SettingsScript settingsScript;
    private TMP_Dropdown resDropdown;
    private Toggle fullscreenToggle;

    [HideInInspector] public Resolution[] resolutions;


    private static SettingsManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            instance.CallStart();
            Destroy(gameObject);
            return;
        }

        instance = this;
        instance.CallStart();
        DontDestroyOnLoad(gameObject);
    }

    private void CallStart()
    {
        try //don't do anything if there's no pause screen in the scene
        {
            settingsScript = Resources.FindObjectsOfTypeAll<SettingsScript>()[0];
        }catch
        {
            return;
        }

        //set fullscreen
        if (!PlayerPrefs.HasKey("fullscreen"))
            PlayerPrefs.SetInt("fullscreen", 1);

        //set audio
        if (!PlayerPrefs.HasKey("volume"))
            PlayerPrefs.SetFloat("volume", .8f);

        //getting all resolutions
        resolutions = Screen.resolutions;

        resDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {   //make nice resolution label
            string option = resolutions[i].width + "x" + resolutions[i].height + " @ " + resolutions[i].refreshRateRatio.ToString().Split(".")[0] + "Hz";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
               resolutions[i].height == Screen.currentResolution.height)
            {
                if (!PlayerPrefs.HasKey("resolution"))
                    currentResIndex = i;
                else
                    currentResIndex = PlayerPrefs.GetInt("resolution");
            }
        }

        resDropdown.AddOptions(options);
        resDropdown.value = currentResIndex;
        resDropdown.RefreshShownValue();

        UpdateSettings();
    }

    private void UpdateSettings()
    {   //fullscreen
        if(PlayerPrefs.GetInt("fullscreen") == 0)
        {
            Screen.fullScreen = false;
            fullscreenToggle.isOn = false;
        }
        else if(PlayerPrefs.GetInt("fullscreen") == 1)
        {
            Screen.fullScreen = true;
            fullscreenToggle.isOn = true;
        }
            
        //resolution (settting resolution is handled in start method)
        Screen.SetResolution(resolutions[PlayerPrefs.GetInt("resolution")].width, resolutions[PlayerPrefs.GetInt("resolution")].height, Screen.fullScreen);
    }
}
