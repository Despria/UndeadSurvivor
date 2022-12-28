using UnityEngine;

public enum SoundState
{
    Title = 0,
    Game
}

[CreateAssetMenu(fileName = "Sound Container", menuName = "Scriptable Objects/Singleton Asset/Sound Container", order = 9)]
public class SoundContainer : SingletonScriptableObject<SoundContainer>
{
    public AudioClip[] Clips;    
}
