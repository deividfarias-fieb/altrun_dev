using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class Options_Controller : MonoBehaviour
{
    [SerializeField] private bool isFullscreen;
    [SerializeField] private int resolutionIndex;
    [SerializeField] private float effectsVolume;
    // Start is called before the first frame update

    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Slider effectVolumeSlider;
    [SerializeField] private Slider generalVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;

    [SerializeField] private Resolution[] resolutions;

    private void OnEnable()
    {
        /*resolutions = Screen.resolutions;
        foreach (Resolution reso in resolutions)
        {
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(reso.ToString()));
        }*/
        InitializeResolutions();
        fullScreenToggle.onValueChanged.AddListener(delegate { OnFullScreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        effectVolumeSlider.onValueChanged.AddListener(delegate { OnEffectsVolumeChange(); });
        generalVolumeSlider.onValueChanged.AddListener(delegate { OnGeneralVolumeChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
    }
    void Start()
    {
        if (resolutionIndex == 0)
        {
            resolutionIndex = resolutionDropdown.value;
        }

        fullScreenToggle.isOn = Screen.fullScreen;

        effectVolumeSlider.value = masterMixer.GetFloat("SFXVolume", out float sfx) ? Mathf.Pow(10, sfx / 20) : 1f;
        generalVolumeSlider.value = masterMixer.GetFloat("Master", out float general) ? Mathf.Pow(10, general / 20) : 1f;
        generalVolumeSlider.value = masterMixer.GetFloat("Music", out float music) ? Mathf.Pow(10, music / 20) : 1f;
    }

    void InitializeResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void OnFullScreenToggle()
    {
        Screen.fullScreen = fullScreenToggle.isOn;
        OnResolutionChange();
    }

    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, fullScreenToggle.isOn);
    }

    public void OnEffectsVolumeChange()
    {
        masterMixer.SetFloat("SFXVolume", Mathf.Log10(effectVolumeSlider.value) * 20);
    }

    public void OnGeneralVolumeChange()
    {
        masterMixer.SetFloat("Master", Mathf.Log10(generalVolumeSlider.value) * 20);
    }
    public void OnMusicVolumeChange()
    {
        masterMixer.SetFloat("Music", Mathf.Log10(musicVolumeSlider.value) * 20);
    }
}
