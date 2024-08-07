using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle vSyncToggle;
    Resolution[] resolutions;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        Close();
        PopulateResolutions();
        LoadSavedSettings();
    }

    private void LoadSavedSettings()
    {
        if (PlayerPrefs.GetInt(OptionStrings.UserChangedVideoSettingsKey) == 1)
        {
            fullscreenToggle.isOn = PlayerPrefs.GetInt(OptionStrings.FullscreenKey) == 1;
            vSyncToggle.isOn = PlayerPrefs.GetInt(OptionStrings.VSyncKey) == 1;
            resolutionDropdown.value = PlayerPrefs.GetInt(OptionStrings.ResolutionKey);
            ApplySettings(false);
        }
    }

    private void PopulateResolutions()
    {
        resolutions = Screen.resolutions;
        for (int i = 0; i < resolutions.Length; i++)
        {
            Debug.Log(resolutions[i].ToString());
            string resString = resolutions[i].width + "x" + resolutions[i].height + " @ " + resolutions[i].refreshRateRatio.value + " Hz";
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(resString));

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                resolutionDropdown.value = i;
            }
        }
    }

    public void ApplySettings(bool playerAction)
    {
        int choice = resolutionDropdown.value;
        FullScreenMode mode = fullscreenToggle.isOn ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.Windowed;
        Screen.SetResolution(resolutions[choice].width, resolutions[choice].height, mode, resolutions[choice].refreshRateRatio);
        QualitySettings.vSyncCount = vSyncToggle.isOn ? 1 : 0;

        if (playerAction)
        {
            PlayerPrefs.SetInt(OptionStrings.UserChangedVideoSettingsKey, 1);
            SaveSettings();
        }
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt(OptionStrings.FullscreenKey, fullscreenToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt(OptionStrings.VSyncKey, vSyncToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt(OptionStrings.ResolutionKey, resolutionDropdown.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close()
    {
        canvas.enabled = false;
        canvasGroup.interactable = false;
    }

    public void Show()
    {
        canvasGroup.interactable = true;
        canvas.enabled = true;
    }
}
