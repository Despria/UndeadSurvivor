using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 여러 게임 오브젝트를 각각의 Pool을 생성하여 관리하는 Object Pooler 클래스.<br/>
/// Queue를 이용해 만들어진 Pool들을 Dictionary를 통해 관리함.
/// </summary>

public class ObjectPooler : MonoBehaviour
{
    /// 각각의 Object Pool을 생성할 때 사용할 Pool 클래스
    /// 프리팹, 태그, Pool의 크기에 대한 정보를 저장
    [System.Serializable]
    private class Pool
    {
        public GameObject objectPrefab;
        public string tag;
        public int size;
    }

    #region Field
    /// Pool 클래스를 모아서 ObjectPooler의 초기화에 사용할 List
    /// 에디터에서 List의 크기를 지정하고 각 Pool 클래스의 값을 초기화함
    [SerializeField] List<Pool> pools;

    /// Pool 전체를 관리하는 poolDictionary
    Dictionary<string, Queue<GameObject>> poolDictionary;

    /// 오브젝트 풀의 용량이 부족할 경우 추가적으로 오브젝트를 생성할 때
    /// 태그를 통해 Pool 클래스에서 오브젝트의 정보를 얻어오기 위한 poolIndex
    Dictionary<string, Pool> poolIndex;
    #endregion

    #region Method
    /// <summary>
    /// 오브젝트를 꺼내지는 않되, 오브젝트의 정보를 확인할 CheckFromPool 메서드.
    /// 태그를 이용해 해당되는 오브젝트를 반환함.
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public GameObject CheckFromPool (string tag)
    {
        if (!poolIndex.ContainsKey(tag))
            return null;

        GameObject objectToCheck = poolIndex[tag].objectPrefab;

        return objectToCheck;
    }

    /// <summary>
    /// Dictionary로부터 오브젝트를 얻어올 SpawnFromPool 메서드.
    /// 태그를 이용해 꺼내올 오브젝트에 해당하는 pool을 찾고, 해당 오브젝트의 OnObjectSpawn 메서드를 실행함.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public GameObject SpawnFromPool (string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
            return null;

        if (poolDictionary[tag].Count > 0)
        {
            GameObject objectToSpawn = poolDictionary[tag].Dequeue();
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
            if (pooledObj != null)
                pooledObj.OnObjectSpawn();

            return objectToSpawn;
        }
        else
        {
            GameObject newObject = Instantiate(poolIndex[tag].objectPrefab);
            newObject.SetActive(true);
            newObject.transform.position = position;
            newObject.transform.rotation = rotation;

            IPooledObject pooledObj = newObject.GetComponent<IPooledObject>();
            if (pooledObj != null)
                pooledObj.OnObjectSpawn();

            return newObject;
        }
    }

    /// <summary>
    /// 사용한 오브젝트를 다시 Dictionary로 집어넣을 InsertToPool 메서드.
    /// 태그를 이용해 돌아갈 pool을 찾고, 해당 pool에 오브젝트를 비활성화시켜 집어넣음.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="obj"></param>
    public void InsertToPool(string tag, GameObject obj)
    {
        IPooledObject objectToInsert = obj.GetComponent<IPooledObject>();
        objectToInsert.OnObjectInsert();

        obj.SetActive(false);
        poolDictionary[tag].Enqueue(obj);
    }
    #endregion

    #region Singleton
    public static ObjectPooler Instance;

    void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;    
    }
    #endregion

    #region Unity Event
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        poolIndex = new Dictionary<string, Pool>();

        /// pools List에 담긴 각 오브젝트들을 오브젝트 풀을 생성하여 집어넣음
        /// 그리고 해당 오브젝트 풀을 다시 poolDictionary에 집어넣음
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.objectPrefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
            poolIndex.Add(pool.tag, pool);
        }
    }
    #endregion
}