using UnityEngine;

[CreateAssetMenu(fileName = "AutoGun Multiple Increase", menuName = "Scriptable Objects/Skill/AutoGun/Multiple Increase", order = 3)]
public class AutoGunMultipleIncrease : AutoGunUpgrade
{
    [SerializeField] int increaseAmount;

    public override void OnSelect()
    {
        Effect();
    }

    protected override void Effect()
    {
        bulletMultiple.runtimeValue += increaseAmount;
    }
}
