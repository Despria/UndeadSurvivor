using System.Collections;
using UnityEngine;

public class CornThrower : GameEventListener
{
    #region Field
    [Header("Corn")]
    [SerializeField] GameObject cornPrefab;

    [Header("Corn Status")]
    [SerializeField] FloatVariable cornDamage;
    [SerializeField] FloatVariable cornThrowDelay;
    [SerializeField] FloatVariable cornExplodeRange;
    [SerializeField] FloatVariable cornFlyingTime;

    [Header("Other Setting")]
    [SerializeField] Vector3Variable playerPosition;
    [SerializeField] FloatVariable monsterDetectionRange;
    #endregion

    #region Unity Event
    new void OnEnable()
    {
        base.OnEnable();
        transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
        transform.SetAsLastSibling();
        transform.position = new Vector3(playerPosition.runtimeValue.x + 0.6f, playerPosition.runtimeValue.y + 0.4f, 0f);
    }

    void Start()
    {
        Response.AddListener(DeActivate);
        StartCoroutine(ThrowBomb());
    }
    #endregion

    #region Method
    IEnumerator ThrowBomb()
    {
        while (true)
        {
            Collider2D[] monsterInRange = Physics2D.OverlapCircleAll(playerPosition.runtimeValue, monsterDetectionRange.runtimeValue, 1 << LayerMask.NameToLayer("Monster"));
            if (monsterInRange.Length > 0) 
            { 
                GameObject cornInPool = ObjectPooler.Instance.SpawnFromPool("Corn", transform.position, Quaternion.identity);
                Corn corn = cornInPool.GetComponent<Corn>();

                int targetIndex = Random.Range(0, monsterInRange.Length);
                Transform target = monsterInRange[targetIndex].transform;
                cornFlyingTime.runtimeValue = Vector2.Distance(transform.position, target.position) / monsterDetectionRange.runtimeValue;

                corn.SetCorn(transform, target, cornFlyingTime.runtimeValue);
            }

            yield return new WaitForSeconds(cornThrowDelay.runtimeValue);
        }
    }

    public void SetCornStatus(float damageToAdd, float delayToDecrease, float explodeRangeToAdd)
    {
        cornDamage.runtimeValue += damageToAdd;
        cornThrowDelay.runtimeValue -= delayToDecrease;
        cornExplodeRange.runtimeValue += explodeRangeToAdd;
    }

    public void DeActivate()
    {
        StartCoroutine(SetActiveFalse());
    }

    IEnumerator SetActiveFalse()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
    #endregion
}