using UnityEngine;

public class AudioEventListener : GameEventListener
{
    [SerializeField] AudioClip audioClip;

    void Start()
    {
        Response.AddListener(PlayOneShot);
    }

    public void PlayOneShot()
    {
        SoundManager.Instance.PlayOneShot(audioClip);
    }
}
