using UnityEngine;

public class ObjectSpawner : GameEventListener
{
    #region Field
    [Header("Object Spawn Condition")]
    [SerializeField] Vector2Variable mapBorderUpLeft;
    [SerializeField] Vector2Variable mapBorderDownRight;

    [Header("Monster Spawn Condition")]
    [SerializeField] IntVariable currentMonsterNumber;
    [SerializeField] float lastMonsterSpawnTime;
    [SerializeField] IntVariable maximumMonsterNumber;
    [SerializeField] FloatVariable monsterSpawnDelay;
    [SerializeField] StringVariable[] monsterTags;

    [Header("Item Spawn Condition")]
    [SerializeField] IntVariable currentHealthPackNumber;
    [SerializeField] IntVariable currentRootBoxNumber;
    [SerializeField] IntVariable currentMagnetNumber;
    [SerializeField] float lastItemSpawnTime;
    [SerializeField] IntVariable maximumHealthPackNumber;
    [SerializeField] IntVariable maximumMagnetNumber;
    [SerializeField] IntVariable maximumRootBoxNumber;
    [SerializeField] FloatVariable itemSpawnDelay;
    [SerializeField] StringVariable[] itemTags;
    #endregion

    #region Unity Event
    void Start()
    {
        Response.AddListener(DeActivate);
    }

    void Update()
    {
        lastMonsterSpawnTime += Time.deltaTime;
        if (lastMonsterSpawnTime > monsterSpawnDelay.runtimeValue &&
            maximumMonsterNumber.runtimeValue > currentMonsterNumber.runtimeValue)
        {
            lastMonsterSpawnTime = 0f;
            SpawnMonster();
        }

        lastItemSpawnTime += Time.deltaTime;
        if (lastItemSpawnTime > itemSpawnDelay.runtimeValue)
        {
            lastItemSpawnTime = 0f;
            SpawnItem();
        }
    }
    #endregion

    #region Method
    public void SpawnMonster()
    {
        float spawnPositionX = Random.Range(mapBorderUpLeft.runtimeValue.x, mapBorderDownRight.runtimeValue.x);
        float spawnPositionY = Random.Range(mapBorderDownRight.runtimeValue.y, mapBorderUpLeft.runtimeValue.y);
        Vector3 spawnPosition = new Vector3(spawnPositionX, spawnPositionY, 0);

        string monsterTag = monsterTags[Random.Range(0, monsterTags.Length)].runtimeValue;

        ObjectPooler.Instance.SpawnFromPool(monsterTag, spawnPosition, Quaternion.identity);
        currentMonsterNumber.runtimeValue++;
    }

    public void SpawnItem()
    {
        float spawnPositionX = Random.Range(mapBorderUpLeft.runtimeValue.x, mapBorderDownRight.runtimeValue.x);
        float spawnPositionY = Random.Range(mapBorderDownRight.runtimeValue.y, mapBorderUpLeft.runtimeValue.y);
        Vector3 spawnPosition = new Vector3(spawnPositionX, spawnPositionY, 0);

        string itemTag = itemTags[Random.Range(0, itemTags.Length)].runtimeValue;

        // itemTags[0] == HealthPack
        if (itemTag == itemTags[0].runtimeValue &&
            maximumHealthPackNumber.runtimeValue > currentHealthPackNumber.runtimeValue)
        {
            ObjectPooler.Instance.SpawnFromPool(itemTag, spawnPosition, Quaternion.identity);
        }

        // itemTags[1] == Magnet
        else if (itemTag == itemTags[1].runtimeValue &&
            maximumMagnetNumber.runtimeValue > currentMagnetNumber.runtimeValue) 
        {
            ObjectPooler.Instance.SpawnFromPool(itemTag, spawnPosition, Quaternion.identity);
        }

        // itemTags[2] == RootBox
        else if (itemTag == itemTags[2].runtimeValue &&
            maximumRootBoxNumber.runtimeValue > currentMagnetNumber.runtimeValue)
        {
            ObjectPooler.Instance.SpawnFromPool(itemTag, spawnPosition, Quaternion.identity);
        }
    }

    // OnPlayerDied에 반응
    public void DeActivate()
    {
        gameObject.SetActive(false);
    }
    #endregion
}