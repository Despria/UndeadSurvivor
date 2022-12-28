using UnityEngine;

[CreateAssetMenu(fileName = "SpinningShovel Damage Increase", menuName = "Scriptable Objects/Skill/SpinningShovel/Damage Increase", order = 1)]
public class SpinningShovelDamageIncrease : SpinningShovelUpgrade
{
    [SerializeField] float increaseAmount;

    public override void OnSelect()
    {
        Effect();
    }

    protected override void Effect()
    {
        shovelDamage.runtimeValue += increaseAmount;
    }
}
