using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    private static float MAX_MUSIC_VOLUME = 0.3f;
    private static float MAX_SFX_VOLUME = 1f;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

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

    public void StartGame()
    {
        SceneManager.LoadScene(1);
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
        Resolution res = filteredResolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
    private void Init()
    {
        qualityDropdown.value = QualitySettings.GetQualityLevel();

        InitResolutions();

        MusicManager.Instance.PlayMusic("MenuMusic");
        OpenSubMenu("MainMenu");

        musicSlider.maxValue = MAX_MUSIC_VOLUME;
        sfxSlider.maxValue = MAX_SFX_VOLUME;
        musicSlider.value = MusicManager.Instance.GetAudioSource().volume;
        sfxSlider.value = SoundEffectManager.Instance.GetAudioSource().volume;
    }

    private void InitResolutions()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        float currentRefreshRate = Screen.currentResolution.refreshRate;

        foreach (var res in resolutions)
            if (res.refreshRate == currentRefreshRate)
                filteredResolutions.Add(res);

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = filteredResolutions[i].width + " x " + filteredResolutions[i].height;
            options.Add(resolutionOption);
            if (filteredResolutions[i].width == Screen.width &&
                filteredResolutions[i].height == Screen.height)
                currentResolutionIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
}
