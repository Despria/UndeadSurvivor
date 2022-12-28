using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Float", menuName = "Scriptable Objects/Variables/Float", order = 1)]
public class FloatVariable : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] float initialValue;
    [NonSerialized] public float runtimeValue;

    public void OnAfterDeserialize()
    {
        runtimeValue = initialValue;
    }

    public void OnBeforeSerialize() { }

    public void ResetValue()
    {
        runtimeValue = initialValue;
    }
}
