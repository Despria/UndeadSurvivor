using UnityEngine;

public class ObjectSpawnerSetting : GameEventListener
{
    #region Field
    [Header("Monster Spawn Setting")]
    [SerializeField] IntVariable maximumMonsterNumber;
    [SerializeField] FloatVariable monsterSpawnDelay;

    [Header("Zombie Status")]
    [SerializeField] FloatVariable zombie_maximumHP;
    [SerializeField] FloatVariable zombie_attackPoint;
    [Header("Zombie2 Status")]
    [SerializeField] FloatVariable zombie2_maximumHP;
    [SerializeField] FloatVariable zombie2_attackPoint;
    [Header("Skeleton Status")]
    [SerializeField] FloatVariable skeleton_maximumHP;
    [SerializeField] FloatVariable skeleton_attackPoint;
    [Header("Skeleton2 Status")]
    [SerializeField] FloatVariable skeleton2_maximumHP;
    [SerializeField] FloatVariable skeleton2_attackPoint;
    [Header("Devil Status")]
    [SerializeField] FloatVariable devil_maximumHP;
    [SerializeField] FloatVariable devil_attackPoint;

    [Header("Item Spawn Setting")]
    [SerializeField] IntVariable maximumHealthPackNumber;
    [SerializeField] IntVariable maximumRootBoxNumber;
    [SerializeField] IntVariable maximumMagnetNumber;
    [SerializeField] FloatVariable itemSpawnDelay;
    #endregion

    #region Unity Event
    void Start()
    {
        Response.AddListener(UpgradeMonsters);

        ResetMonsterStatus();
    }
    #endregion

    #region Method
    public void UpgradeMonsters()
    {
        UIManager.Instance.OpenPopUpLine("몬스터가 강화됩니다!", Vector3.up * 100, 1.5f);

        IncreaseMaximumMonsterNumber();
        DecreaseMonsterSpawnDelay();
        EnhanceMonsters();
    }

    void IncreaseMaximumMonsterNumber()
    {
        if (maximumMonsterNumber.runtimeValue < 500)
            maximumMonsterNumber.runtimeValue += 10;
    }

    void DecreaseMonsterSpawnDelay()
    {
        if (monsterSpawnDelay.runtimeValue > 0.3f)
            monsterSpawnDelay.runtimeValue -= 0.1f;
    }

    void EnhanceMonsters()
    {
        zombie_maximumHP.runtimeValue += 5f;
        zombie_attackPoint.runtimeValue += 1;

        zombie2_maximumHP.runtimeValue += 7f;
        zombie2_attackPoint.runtimeValue += 1.5f;

        skeleton_maximumHP.runtimeValue += 5f;
        skeleton_attackPoint.runtimeValue += 1.5f;

        skeleton2_maximumHP.runtimeValue += 7f;
        skeleton2_attackPoint.runtimeValue += 2f;

        devil_maximumHP.runtimeValue += 5;
        devil_attackPoint.runtimeValue += 2.5f;
    }

    void ResetMonsterStatus()
    {
        zombie_maximumHP.ResetValue();
        zombie_attackPoint.ResetValue();
        zombie2_maximumHP.ResetValue();
        zombie2_attackPoint.ResetValue();
        skeleton_maximumHP.ResetValue();
        skeleton_attackPoint.ResetValue();
        skeleton2_maximumHP.ResetValue();
        skeleton2_attackPoint.ResetValue();
        devil_maximumHP.ResetValue();
        devil_attackPoint.ResetValue();
    }
    #endregion
}