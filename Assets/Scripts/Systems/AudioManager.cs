using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton 
    
    private AudioSource audioSource;
    private AudioSource musicSource; // background music

    void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;  // Enable looping for background music
    }

    // Function to play a sound
    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }

    public void PlayMusic(AudioClip clip, float volume = 0.5f)
    {
        if (clip != null)
        {
            if (musicSource.isPlaying)
                musicSource.Stop(); // Stop any existing music

            musicSource.clip = clip;
            musicSource.volume = Mathf.Clamp01(volume);
            musicSource.Play(); // Start playing the new music
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

}
