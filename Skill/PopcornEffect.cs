using UnityEngine;

public class PopcornEffect : MonoBehaviour, IPooledObject
{
    [SerializeField] ParticleSystem popcornEffect;

    public void OnParticleSystemStopped()
    {
        ObjectPooler.Instance.InsertToPool("Popcorn", gameObject);
    }

    public void OnObjectSpawn()
    {
        popcornEffect.Play();
    }

    public void OnObjectInsert() { }
}
