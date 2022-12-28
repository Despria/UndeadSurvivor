using UnityEngine;

[CreateAssetMenu(fileName = "AutoGun Damage Increase", menuName ="Scriptable Objects/Skill/AutoGun/Damage Increase", order = 1)]
public class AutoGunDamageIncrease : AutoGunUpgrade
{
    [SerializeField] float increaseAmount;

    public override void OnSelect()
    {
        Effect();
    }

    protected override void Effect()
    {
        bulletDamage.runtimeValue += increaseAmount;
    }
}
