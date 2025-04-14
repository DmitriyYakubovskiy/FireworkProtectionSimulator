using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioMixerGroup mixerGroup;
    [SerializeField] protected float volume = 0.2f;
    private AudioSource audioSource;

    public SoundArray[] sounds;
    public float Volume => volume;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixerGroup;
    }

    public void PlaySoundForUI()
    {
        if (sounds.Length == 0) return;
        AudioClip clip = sounds[0].soundArray[Random.Range(0, sounds[0].soundArray.Length)];
        audioSource.pitch = Random.Range(0.85f, 1.2f);
        audioSource.PlayOneShot(clip, volume);
    }

    public void PlaySound(int i = 0, float volume = 1f, float p1 = 0.85f, float p2 = 1.2f, bool isDestroyed = false)
    {
        int index = Random.Range(0, sounds[i].soundArray.Length);
        AudioClip clip = sounds[i].soundArray[index];
        audioSource.pitch = Random.Range(p1, p2);
        if (isDestroyed)
        {
            GameObject soundObj = new GameObject("Sound");
            soundObj.transform.position = transform.position;
            Instantiate(soundObj);
            AudioSource source = soundObj.AddComponent<AudioSource>();
            source.clip = clip;
            source.outputAudioMixerGroup = mixerGroup;
            source.spatialBlend = 1f;
            source.volume = volume;
            source.pitch = Random.Range(p1, p2);
            source.Play();
            Destroy(soundObj, clip.length);
        }
        else
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }

    public void AudioStop()
    {
        if (audioSource.isPlaying) audioSource.Stop();
    }

    public void AudioPause()
    {
        if (audioSource.isPlaying) audioSource.Pause();
    }

    public void AudioStart(int index = -1, float v = 0.2f)
    {
        if (!audioSource.isPlaying)
        {
            if (index == -1) audioSource.UnPause();
            else PlaySound(index, volume = v);
        }
    }

    [System.Serializable]
    public class SoundArray
    {
        public AudioClip[] soundArray;
    }
}