using UnityEngine;

[CreateAssetMenu(fileName = "AutoGun Delay Decrease", menuName = "Scriptable Objects/Skill/AutoGun/Delay Decrease", order = 2)]
public class AutoGunDelayDecrease : AutoGunUpgrade
{
    [SerializeField] float decreaseAmount;

    public override void OnSelect()
    {
        Effect();
    }

    protected override void Effect()
    {
        bulletShotDelay.runtimeValue -= decreaseAmount;
    }
}
