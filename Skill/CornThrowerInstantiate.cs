using UnityEngine;

[CreateAssetMenu(fileName = "CornThrower Instantiate", menuName = "Scriptable Objects/Skill/CornThrower/Instantiate", order = 0)]
public class CornThrowerInstantiate : CornThrowerUpgrade
{
    [SerializeField] Vector3Variable playerPosition;

    public override void OnSelect()
    {
        Effect();
    }

    protected override void Effect()
    {
        cornDamage.ResetValue();
        cornExplodeRange.ResetValue();
        cornThrowDelay.ResetValue();

        ObjectPooler.Instance.SpawnFromPool("CornThrower", playerPosition.runtimeValue + new Vector3(0.5f, 0.2f), Quaternion.identity);
    }
}
