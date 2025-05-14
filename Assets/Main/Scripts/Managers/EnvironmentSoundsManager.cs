using UnityEngine;

public class EnvironmentSoundsManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] environmentSounds;
    [SerializeField] private float volume = 0.2f;
    [SerializeField] private float minDelay = 5f; 
    [SerializeField] private float maxDelay = 15f;

    private AudioSource audioSource;
    private int currentSoundIndex = 0;
    private float nextPlayTime;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;

        PlayNextSound();
    }

    private void Update()
    {
        if (!audioSource.isPlaying && Time.time >= nextPlayTime)
        {
            PlayNextSound();
        }
    }

    private void PlayNextSound()
    {
        if (environmentSounds.Length == 0) return;

        audioSource.clip = environmentSounds[currentSoundIndex];
        audioSource.Play();

        currentSoundIndex = (currentSoundIndex + 1) % environmentSounds.Length;

        nextPlayTime = Time.time + audioSource.clip.length + Random.Range(minDelay, maxDelay);
    }
}
