using UnityEngine;

public class MusicController : MonoBehaviour
{
    private static MusicController instance;

    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Mantener la m�sica entre escenas
    }

    public void PlayMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    public void SetVolume(float volume)
    {
        musicSource.volume = volume;
    }
}
