using UnityEngine;

[CreateAssetMenu(fileName = "CornThrower Damage Increase", menuName = "Scriptable Objects/Skill/CornThrower/Damage Increase", order = 1)]
public class CornThrowerDamageIncrease : CornThrowerUpgrade
{
    [SerializeField] float increaseAmount;

    public override void OnSelect()
    {
        Effect();
    }

    protected override void Effect()
    {
        cornDamage.runtimeValue += increaseAmount;
    }
}
