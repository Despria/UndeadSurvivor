using UnityEngine;

public abstract class SpinningShovelUpgrade : SkillUpgrade
{
    [SerializeField] protected FloatVariable shovelDamage;
    [SerializeField] protected FloatVariable shovelSpeed;
    [SerializeField] protected IntVariable shovelNumber;
}
