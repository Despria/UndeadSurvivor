using UnityEngine;

[CreateAssetMenu(fileName = "CornThrower Delay Decrease", menuName = "Scriptable Objects/Skill/CornThrower/Delay Decrease", order = 2)]
public class CornThrowerDelayDecrease : CornThrowerUpgrade
{
    [SerializeField] float decreaseAmount;

    public override void OnSelect()
    {
        Effect();
    }

    protected override void Effect()
    {
        cornThrowDelay.runtimeValue -= decreaseAmount;
    }
}
