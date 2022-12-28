using System.Collections;
using UnityEngine;

public class Corn : MonoBehaviour, IPooledObject
{
    [SerializeField] FloatVariable cornDamage;
    [SerializeField] FloatVariable cornExplodeRange;

    [SerializeField] FloatVariable cornRotationSpeed;
    [SerializeField] FloatVariable cornRemainingTime;
    [SerializeField] FloatVariable curveAmount;
    [SerializeField] FloatVariable cornFlyingTime;
    float startTime;

    Transform start;
    Transform target;
    Vector3 startPosition;
    Vector3 targetPosition;

    [SerializeField] ParticleSystem popcornEffect;
    [SerializeField] GameEvent OnCornExplode;

    #region Unity Event
    void Update()
    {
        ThrowCorn();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform == target)
        {
            ExplodeCorn();
            ObjectPooler.Instance.SpawnFromPool("Popcorn", transform.position, Quaternion.identity);
            ObjectPooler.Instance.InsertToPool("Corn", gameObject);
        }
    }
    #endregion

    #region Method
    public void SetCorn(Transform start, Transform target, float cornFlyingTime)
    {
        this.start = start;
        this.target = target;
        this.cornFlyingTime.runtimeValue = cornFlyingTime;
    }

    void ThrowCorn()
    {
        transform.Rotate(new Vector3(0f, 0f, cornRotationSpeed.runtimeValue * Time.deltaTime));

        startPosition = start.position;
        targetPosition = target.position;

        Vector3 center = (startPosition + targetPosition) * 0.5f;
        center.y -= curveAmount.runtimeValue;
        startPosition -= center;
        targetPosition -= center;

        float fracComplete = (Time.time - startTime) / cornFlyingTime.runtimeValue;

        transform.position = Vector3.Slerp(startPosition, targetPosition, fracComplete);
        transform.position += center;
    }

    void ExplodeCorn()
    {
        OnCornExplode.Raise();
        Collider2D[] monsterInRange = Physics2D.OverlapCircleAll(transform.position, cornExplodeRange.runtimeValue, 1 << LayerMask.NameToLayer("Monster"));

        if (monsterInRange != null)
        {
            for (int i = 0; i < monsterInRange.Length; i++)
            {
                Monster monster = monsterInRange[i].gameObject.GetComponent<Monster>();
                monster.GetDamage(cornDamage.runtimeValue);
            }
        }
    }

    IEnumerator ReturnToPool()
    {
        yield return new WaitForSeconds(cornRemainingTime.runtimeValue);
        ObjectPooler.Instance.InsertToPool("Bullet", gameObject);
    }

    public void OnObjectSpawn()
    {
        startTime = Time.time;
        StartCoroutine(ReturnToPool());
    }

    public void OnObjectInsert() { }
    #endregion
}