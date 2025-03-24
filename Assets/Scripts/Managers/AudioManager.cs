using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton 
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource musicSource; // background music
    [SerializeField] private List<SoundEntry> soundEntries;  // List of sounds in Inspector

    private Dictionary<SoundType, AudioClip> soundDictionary;
    private Dictionary<Colors, SoundType> colorToSoundMap;  // Mapping Colors -> SoundType

    private Queue<AudioClip> soundQueue = new Queue<AudioClip>(); // Queue sounds so they dont overlap
    private bool soundIsPlaying = false;

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

        // Convert list to dictionary for fast lookup
        soundDictionary = new Dictionary<SoundType, AudioClip>();
        foreach (var entry in soundEntries)
        {
            if (!soundDictionary.ContainsKey(entry.soundType))
                soundDictionary.Add(entry.soundType, entry.audioClip);
        }

        // Initialize color-to-sound mapping
        colorToSoundMap = new Dictionary<Colors, SoundType>
        {
            { Colors.Red, SoundType.Red },
            { Colors.Yellow, SoundType.Yellow },
            { Colors.Blue, SoundType.Blue },
            { Colors.Green, SoundType.Green },
            { Colors.Orange, SoundType.Orange },
            { Colors.Purple, SoundType.Purple },
        };

        musicSource.loop = true;  // Enable looping for background music
    }

    // Function to play a sound
    public void PlaySound(SoundType type, float volume = 1f)
    {
        if (soundDictionary.TryGetValue(type, out AudioClip clip))
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"No audio clip assigned for {type}");
        }
    }

    // play sounds in order from queue
    private IEnumerator PlayQueuedSounds()
    {
        soundIsPlaying = true;

        while (soundQueue.Count > 0)
        {
            AudioClip clip = soundQueue.Dequeue();
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
        }

        soundIsPlaying = false;
    }


    // Play sound based on Colors enum
    public SoundType GetSoundTypeByColor(Colors color)
    {
        if (colorToSoundMap.TryGetValue(color, out SoundType soundType))
        {
            return(soundType);
        }
        else
        {
            Debug.LogWarning($"No sound mapped for color {color}");
            return SoundType.NULL;
        }
    }

    // Play multiple clips sequentially
    public void PlaySoundsSequentially(params SoundType[] types)
    {
        foreach (SoundType type in types)
        {
            if (soundDictionary.TryGetValue(type, out AudioClip clip))
            {
                Debug.Log("Play SOUND: " + type);
                soundQueue.Enqueue(clip);
                if (!soundIsPlaying) StartCoroutine(PlayQueuedSounds());
            }
            else
            {
                Debug.LogWarning($"No audio clip assigned for {type}");
            }
        }
    }

    public void PlayMusic(SoundType type, float volume = 0.5f)
    {
        if (soundDictionary.TryGetValue(type, out AudioClip clip))
        {
            if (musicSource.isPlaying)
                musicSource.Stop(); // Stop any existing music

            musicSource.clip = clip;
            musicSource.volume = Mathf.Clamp01(volume);
            musicSource.Play(); // Start playing the new music
        }
        else
        {
            Debug.LogWarning($"No audio clip assigned for {type}");
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

}

// Serializable struct for assignment in Inspector
[System.Serializable]
public class SoundEntry
{
    public SoundType soundType;
    public AudioClip audioClip;
}