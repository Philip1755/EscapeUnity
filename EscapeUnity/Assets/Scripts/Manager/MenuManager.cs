using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    private static float MAX_MUSIC_VOLUME = 0.3f;
    private static float MAX_SFX_VOLUME = 1f;

    private Resolution[] resolutions;

    [SerializeField] private GameObject[] subMenus;
    [SerializeField] private Slider musicSlider, sfxSlider;
    [SerializeField] private TMP_Dropdown resolutionDropdown, qualityDropdown;

    private void Start()
    {
        Instance = this;
        Init();
    }

    public void PlayClickSound()
    {
        SoundEffectManager.Instance.PlayClip("ButtonClick");
    }

    public void OpenSubMenu(string name)
    {
        foreach (var subMenu in subMenus)
            subMenu.SetActive(subMenu.name.Equals(name));
    }

    public void ExitGame() => Application.Quit();

    public void ChangeMusicVolume(float value)
        => MusicManager.Instance.GetAudioSource().volume = value;

    public void ChangeSFXVolume(float value)
        => SoundEffectManager.Instance.GetAudioSource().volume = value;

    public void SetQuality(int qualityIndex)
        => QualitySettings.SetQualityLevel(qualityIndex);

    public void SetFullScreen(bool isFullScreen)
        => Screen.fullScreen = isFullScreen;

    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
    private void Init()
    {
        qualityDropdown.value = QualitySettings.GetQualityLevel();

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
                currentResIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();

        MusicManager.Instance.PlayMusic("MenuMusic");
        OpenSubMenu("MainMenu");

        musicSlider.maxValue = MAX_MUSIC_VOLUME;
        sfxSlider.maxValue = MAX_SFX_VOLUME;
        musicSlider.value = MusicManager.Instance.GetAudioSource().volume;
        sfxSlider.value = SoundEffectManager.Instance.GetAudioSource().volume;
    }
}
