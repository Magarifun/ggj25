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

    private int lastSfxIndex;

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

    public void PlaySoundEffectVariant(int[] soundIndeces)
    {
        int randomIndex = Random.Range(0, soundIndeces.Length);
        PlaySoundEffect(soundIndeces[randomIndex]);
    }

    /// <summary>
    /// Play a sound effect by passing its index.
    /// </summary>
    /// <param name="soundIndex">Index of the sound in the array.</param>
    public void PlaySoundEffect(int soundIndex)
    {
        lastSfxIndex = soundIndex;
        if (soundIndex >= 0 && soundIndex < soundEffects.Length)
        {
            effectsSource.PlayOneShot(soundEffects[soundIndex]);
        }
        else
        {
            Debug.LogWarning($"Sound index {soundIndex} is out of range.");
        }
    }

    public void PlayLastSoundEffect() => PlaySoundEffect(lastSfxIndex);

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

    public void AnimalSfx() => PlaySoundEffect(0);
    public void ShakeSfx() => PlaySoundEffect(1);
    public void RollSfx() => PlaySoundEffect(2);
    public void RandomizeSfx() => PlaySoundEffect(3);
    public void RockSfx() => PlaySoundEffect(4);
    public void FireSfx() => PlaySoundEffect(5);
    public void WaterSfx() => PlaySoundEffect(6);
    public void SunSfx() => PlaySoundEffect(7);
    public void ThunderSfx() => PlaySoundEffect(8);
    public void TreeSfx() => PlaySoundEffect(9);
    public void TideSfx() => PlaySoundEffect(10);
    public void HumanSfx() => PlaySoundEffectVariant(new int[] { 11, 12, 13, 14, 15, 16, 17 });
    public void SeedSfx() => PlaySoundEffect(18);
    public void ChorusSfx() => PlaySoundEffect(19);
    public void PopSfx() => PlaySoundEffectVariant(new int[] { 20, 21 });
    public void ClickSfx() => PlaySoundEffect(22);
}
