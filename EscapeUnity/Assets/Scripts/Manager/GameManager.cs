using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Start()
    {
        Instance = this;
        MusicManager.Instance.GetAudioSource().clip = null;
    }
}
