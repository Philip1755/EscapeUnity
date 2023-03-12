using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject[] subMenus;

    private bool isPaused = false;

    private void Start()
    {
        pauseMenu.SetActive(isPaused);
        OpenSubMenu("MainMenu");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame();
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pauseMenu.SetActive(isPaused);
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

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
