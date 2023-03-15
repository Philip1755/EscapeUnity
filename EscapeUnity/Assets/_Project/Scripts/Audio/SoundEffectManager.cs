using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance;

    [SerializeField] private AudioClip[] sounds;

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

    public void PlayClip(string name)
    {
        foreach (var sound in sounds)
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
