using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] subMenus;
    [SerializeField] private Slider loadingSlider;

    private void Start()
    {
        MusicManager.Instance.PlayMusic("MenuMusic");
        OpenSubMenu("MainMenu");
    }

    public void PlayClickSound()
    {
        SoundEffectManager.Instance.PlayClip("ButtonClick");
    }

    public void StartGame()
    {
        OpenSubMenu("LoadingMenu");
        StartCoroutine(LoadAsny("Game"));
    }

    public void OpenSubMenu(string name)
    {
        foreach (var subMenu in subMenus)
            subMenu.SetActive(subMenu.name.Equals(name));
    }

    public void ExitGame() => Application.Quit();

    private IEnumerator LoadAsny(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01((float)operation.progress / .9f);

            loadingSlider.value = progress;

            yield return null;
        }
    }
}
