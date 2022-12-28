using System;
using UnityEngine;

[CreateAssetMenu(fileName = "String", menuName = "Scriptable Objects/Variables/String", order = 3)]
public class StringVariable : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] string initialValue;
    [NonSerialized] public string runtimeValue;

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
