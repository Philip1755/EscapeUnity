using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject[] subMenus;
    [SerializeField] private AudioClip buttonClick;

    public static bool isPaused = false;

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
        AudioManager.Instance.PlaySoundEffect(buttonClick);
    }

    public void OpenSubMenu(string name)
    {
        foreach (var subMenu in subMenus)
            subMenu.SetActive(subMenu.name.Equals(name));
    }

    public void BackToMenu()
    {
        isPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
