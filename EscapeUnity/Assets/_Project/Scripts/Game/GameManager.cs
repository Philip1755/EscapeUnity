using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        MusicManager.Instance.GetAudioSource().clip = null;
    }

    private void Update()
    {

    }
}
