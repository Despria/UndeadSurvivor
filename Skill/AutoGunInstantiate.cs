using UnityEngine;

[CreateAssetMenu(fileName = "AutoGun Instantiate", menuName = "Scriptable Objects/Skill/AutoGun/Instantiate", order = 0)]
public class AutoGunInstantiate : AutoGunUpgrade
{
    [SerializeField] Vector3Variable playerPosition;

    public override void OnSelect()
    {
        Effect();
    }

    protected override void Effect()
    {
        bulletDamage.ResetValue();
        bulletMultiple.ResetValue();
        bulletShotDelay.ResetValue();

        ObjectPooler.Instance.SpawnFromPool("AutoGun", playerPosition.runtimeValue + new Vector3(0.2f, -0.2f), Quaternion.identity);
    }
}
