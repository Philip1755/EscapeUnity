using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] subMenus;

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
        SceneManager.LoadScene("Game");
    }

    public void OpenSubMenu(string name)
    {
        foreach (var subMenu in subMenus)
            subMenu.SetActive(subMenu.name.Equals(name));
    }

    public void ExitGame() => Application.Quit();
}
