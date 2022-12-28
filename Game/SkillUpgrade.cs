using UnityEngine;

public abstract class SkillUpgrade : ScriptableObject
{
    public Sprite image;
    public string description;

    protected abstract void Effect();
    public abstract void OnSelect();
}
