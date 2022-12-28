using UnityEngine;

[CreateAssetMenu(fileName = "SpinningShovel Number Increase", menuName = "Scriptable Objects/Skill/SpinningShovel/Number Increase", order = 3)]
public class SpinningShovelNumberIncrease : SpinningShovelUpgrade
{
    [SerializeField] int increaseAmount;

    public override void OnSelect()
    {
        Effect();
    }

    protected override void Effect()
    {
        shovelNumber.runtimeValue += increaseAmount;
        GameObject.FindGameObjectWithTag("SpinningShovel").GetComponent<SpinningShovel>().SetShovelNumber(shovelNumber.runtimeValue);
    }
}
