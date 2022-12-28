using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Int", menuName = "Scriptable Objects/Variables/Int", order = 0)]
public class IntVariable : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] int initialValue;
    [NonSerialized] public int runtimeValue;

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