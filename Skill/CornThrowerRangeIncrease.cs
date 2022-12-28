using UnityEngine;

[CreateAssetMenu(fileName = "CornThrower Range Increase", menuName = "Scriptable Objects/Skill/CornThrower/Range Increase", order = 3)]
public class CornThrowerRangeIncrease : CornThrowerUpgrade
{
    [SerializeField] float increaseAmount;

    public override void OnSelect()
    {
        Effect();
    }

    protected override void Effect()
    {
        cornExplodeRange.runtimeValue += increaseAmount;
    }
}
