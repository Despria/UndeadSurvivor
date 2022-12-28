using UnityEngine;

[CreateAssetMenu(fileName = "SpinningShovel Instantiate", menuName = "Scriptable Objects/Skill/SpinningShovel/Instantiate", order = 0)]
public class SpinningShovelInstantiate : SpinningShovelUpgrade
{
    [SerializeField] Vector3Variable playerPosition;

    public override void OnSelect()
    {
        Effect();
    }

    protected override void Effect()
    {
        shovelNumber.ResetValue();
        shovelDamage.ResetValue();
        shovelSpeed.ResetValue();

        ObjectPooler.Instance.SpawnFromPool("SpinningShovel", playerPosition.runtimeValue, Quaternion.identity);
    }
}
