using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Vector3", menuName = "Scriptable Objects/Variables/Vector3", order = 9)]
public class Vector3Variable : ScriptableObject
{
    [SerializeField] Vector3 initialValue;
    [NonSerialized] public Vector3 runtimeValue;

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
