using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    private static float MAX_MUSIC_VOLUME = 0.3f;
    private static float MAX_SFX_VOLUME = 1f;

    [SerializeField] private GameObject[] subMenus;
    [SerializeField] private Slider musicSlider, sfxSlider;

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

    private void Init()
    {
        MusicManager.Instance.PlayMusic("MenuMusic");
        OpenSubMenu("MainMenu");

        musicSlider.maxValue = MAX_MUSIC_VOLUME;
        sfxSlider.maxValue = MAX_SFX_VOLUME;
        musicSlider.value = MusicManager.Instance.GetAudioSource().volume;
        sfxSlider.value = SoundEffectManager.Instance.GetAudioSource().volume;
    }
}
