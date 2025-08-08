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
    }
    void Start()
    {
        if (resolutionIndex == 0)
        {
            resolutionIndex = resolutionDropdown.value;
        }

        fullScreenToggle.isOn = Screen.fullScreen;

        if (masterMixer.GetFloat("SFXVolume", out float volume))
        {
            effectVolumeSlider.value = Mathf.Pow(10, volume / 20);
        }
        else
        {
            // Se não, assume o valor padrão, por exemplo, 1
            effectVolumeSlider.value = 1f;
        }
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

    // Update is called once per frame
    void Update()
    {
        
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
}
