using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Bool", menuName = "Scriptable Objects/Variables/Bool", order = 2)]
public class BoolVariable : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] bool initialValue;
    [NonSerialized] public bool runtimeValue;

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
