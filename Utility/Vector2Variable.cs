using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Vector2", menuName = "Scriptable Objects/Variables/Vector2", order = 8)]
public class Vector2Variable : ScriptableObject
{
    [SerializeField] Vector2 initialValue;
    [NonSerialized] public Vector2 runtimeValue;

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