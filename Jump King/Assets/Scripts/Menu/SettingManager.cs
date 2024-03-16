using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class SettingsManager : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public TMP_Dropdown resolutionDropdown;
    public Button applyButton;
    public Slider musicVolumeSlider;
    public GameSettings gameSettings;
    public Resolution[] screenResolutions;

    public GameObject settingMenu;

    void OnEnable() 
    {
        gameSettings = new GameSettings();
        fullscreenToggle.onValueChanged.AddListener(delegate {OnFullScreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate {OnResolutionChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate {OnMusicVolumeChange(); });
        List<string> resolutions = new List<string>();
        screenResolutions = Screen.resolutions;

        foreach (var resolution in screenResolutions)
        {
            string resolutionString = resolution.width + " x " + resolution.height;
            resolutions.Add(resolutionString);
        }

        // Clear existing options in the TMP Dropdown
        resolutionDropdown.ClearOptions();

        // Add the resolutions to the TMP Dropdown
        resolutionDropdown.AddOptions(resolutions);
    }

    public void OnFullScreenToggle()
    {
        gameSettings.fullscreen = Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void OnResolutionChange()
    {
        Screen.SetResolution(screenResolutions[resolutionDropdown.value].width, screenResolutions[resolutionDropdown.value].height, Screen.fullScreen);
    }

    public void OnMusicVolumeChange()
    {

    }

    public void OnApply()
    {
        settingMenu.SetActive(false);
    }

    public void SaveSetting()
    {

    }
    public void LoadSetting()
    {

    }
}
