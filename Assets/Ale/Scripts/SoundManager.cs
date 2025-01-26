using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // Singleton instance

    [Header("Audio Sources")]
    public AudioSource musicSource;      // Source for background music
    public AudioSource effectsSource;    // Source for sound effects

    [Header("Sound Clips")]
    public AudioClip[] soundEffects;     // Array of sound effects
    public AudioClip backgroundMusic;    // Background music clip

    private void Awake()
    {
        // Implement the Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    private void Start()
    {
        // Start playing background music if assigned
        if (backgroundMusic != null)
        {
            PlayBackgroundMusic();
        }
    }

    /// <summary>
    /// Play a sound effect by passing its index.
    /// </summary>
    /// <param name="soundIndex">Index of the sound in the array.</param>
    public void PlaySoundEffect(int soundIndex)
    {
        if (soundIndex >= 0 && soundIndex < soundEffects.Length)
        {
            effectsSource.PlayOneShot(soundEffects[soundIndex]);
        }
        else
        {
            Debug.LogWarning($"Sound index {soundIndex} is out of range.");
        }
    }

    /// <summary>
    /// Play background music in a loop.
    /// </summary>
    public void PlayBackgroundMusic()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Background music or music source is not assigned.");
        }
    }

    /// <summary>
    /// Stop the background music.
    /// </summary>
    public void StopBackgroundMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }
}
