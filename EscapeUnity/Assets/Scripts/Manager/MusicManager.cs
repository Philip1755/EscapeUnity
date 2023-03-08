using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private AudioClip[] music;

    private AudioSource source;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        source = GetComponent<AudioSource>();
    }

    public void PlayMusic(string name)
    {
        foreach (var sound in music)
        {
            if (sound.name.Equals(name))
            {
                source.clip = sound;
                break;
            }
        }
        source.Play();
    }

    public AudioSource GetAudioSource() => source;
}
