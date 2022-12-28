using UnityEngine;

public abstract class CornThrowerUpgrade : SkillUpgrade
{
    [SerializeField] protected FloatVariable cornDamage;
    [SerializeField] protected FloatVariable cornThrowDelay;
    [SerializeField] protected FloatVariable cornExplodeRange;
}
