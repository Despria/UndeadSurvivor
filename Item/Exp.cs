using UnityEngine;

public class Exp : MonoBehaviour, ICollectable, IPooledObject
{
    [SerializeField] StringVariable itemTag;
    [SerializeField] FloatVariable playerExp;
    [SerializeField] FloatVariable exp;

    public void Get()
    {
        playerExp.runtimeValue += exp.runtimeValue;
        ObjectPooler.Instance.InsertToPool(itemTag.runtimeValue, gameObject);
    }

    public void OnObjectInsert() { }

    public void OnObjectSpawn() { }
}
