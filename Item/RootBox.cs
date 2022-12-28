using UnityEngine;

public class RootBox : MonoBehaviour, ICollectable, IPooledObject
{
    [SerializeField] StringVariable itemTag;

    public void Get()
    {
        GameSceneUI.Instance.OpenSkillSelect();
        ObjectPooler.Instance.InsertToPool(itemTag.runtimeValue, gameObject);
    }

    public void OnObjectInsert() { }

    public void OnObjectSpawn() { }
}
