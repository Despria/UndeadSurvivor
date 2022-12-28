using UnityEngine;

[CreateAssetMenu(fileName = "SpinningShovel Speed Increase", menuName = "Scriptable Objects/Skill/SpinningShovel/Speed Increase", order = 2)]
public class SpinningShovelSpeedIncrease : SpinningShovelUpgrade
{
    [SerializeField] float increaseAmount;

    public override void OnSelect()
    {
        Effect();
    }

    protected override void Effect()
    {
        shovelSpeed.runtimeValue += increaseAmount;
    }
}
