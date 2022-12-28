using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : DontDestroyOnLoadSingleton<SoundManager>
{
    [Header("AudioSource Attached in SoundManager")]
    [SerializeField] AudioSource audioSource;

    [Header("AudioMixer of AudioSource Output")]
    [SerializeField] AudioMixer audioMixer;

    #region Singleton
    new void Awake()
    {
        base.Awake();

        audioSource = GetComponent<AudioSource>();
    }
    #endregion

    #region Method
    public void SetClip(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
    }

    public void SetVolume(float volume) 
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void PlayLoopSound(AudioClip audioClip)
    {
        SetClip(audioClip);
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayOneShot(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void PlayOnAwake(bool isPlayOnAwake)
    {
        audioSource.playOnAwake = isPlayOnAwake;
    }

    public void PauseSound()
    {
        audioSource.Stop();
    }
    #endregion
}