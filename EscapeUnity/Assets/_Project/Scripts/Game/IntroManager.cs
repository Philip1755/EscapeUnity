using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer vp;

    private int pauseTimer = 0;

    private void Update()
    {
        CheckVideoOver();
    }

    private void CheckVideoOver()
    {
        if (vp.isPaused) pauseTimer++;

        if (pauseTimer > 500 || (Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.F) && Input.GetKey(KeyCode.Alpha4)))
            SceneManager.LoadScene("Menu");
    }
}
