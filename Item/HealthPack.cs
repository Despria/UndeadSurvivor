using UnityEngine;

public class HealthPack : MonoBehaviour, ICollectable, IPooledObject
{
    [SerializeField] StringVariable itemTag;
    [SerializeField] FloatVariable playerHp;
    [SerializeField] FloatVariable playerMaximumHp;
    [SerializeField] FloatVariable hp;

    public void Get()
    {
        playerHp.runtimeValue += hp.runtimeValue;
        if (playerHp.runtimeValue > playerMaximumHp.runtimeValue)
        {
            playerHp.runtimeValue = playerMaximumHp.runtimeValue;
        }
        ObjectPooler.Instance.InsertToPool(itemTag.runtimeValue, gameObject);
    }

    public void OnObjectInsert() { }

    public void OnObjectSpawn() { }
}
