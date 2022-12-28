using UnityEngine;

public abstract class AutoGunUpgrade : SkillUpgrade
{
    [SerializeField] protected FloatVariable bulletDamage;
    [SerializeField] protected FloatVariable bulletShotDelay;
    [SerializeField] protected IntVariable bulletMultiple;
}
