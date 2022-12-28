using System.Collections;
using UnityEngine;

public class AutoGun : GameEventListener
{
    #region Field
    [Header("Bullet")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletFirePosition;

    [Header("Bullet Status")]
    [SerializeField] FloatVariable bulletDamage;
    [SerializeField] FloatVariable bulletSpeed;
    [SerializeField] FloatVariable bulletShotDelay;
    [SerializeField] IntVariable bulletMultiple;

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
        transform.position = new Vector3(playerPosition.runtimeValue.x + 0.28f, playerPosition.runtimeValue.y - 0.09f, 0f);
    }

    void Start()
    {
        Response.AddListener(DeActivate);
        StartCoroutine(FireBullet(bulletMultiple.runtimeValue));
    }
    #endregion

    #region Method
    IEnumerator FireBullet(int bulletNumber)
    {
        int centerBullet = Mathf.RoundToInt(bulletMultiple.runtimeValue / 2);

        while (true)
        {
            Collider2D monsterInRange = Physics2D.OverlapCircle(playerPosition.runtimeValue, monsterDetectionRange.runtimeValue, 1 << LayerMask.NameToLayer("Monster"));
            if (monsterInRange != null)
            {
                Vector3 direction = monsterInRange.transform.position - bulletFirePosition.position;

                for (int i = 0; i < bulletMultiple.runtimeValue; i++)
                {
                    GameObject bulletInPool = ObjectPooler.Instance.SpawnFromPool("Bullet", bulletFirePosition.position, Quaternion.identity);
                    Bullet bullet = bulletInPool.GetComponent<Bullet>();

                    float revisedDegree = (i - centerBullet) / 2f;
                    if (bullet != null)
                        bullet.SetBullet(direction + Vector3.right * revisedDegree);
                }
            }

            yield return new WaitForSeconds(bulletShotDelay.runtimeValue);
        }
    }

    public void SetGunStatus(float damageToAdd, float delayToDecrease, int additionalBullet)
    {
        StopAllCoroutines();

        bulletDamage.runtimeValue += damageToAdd;
        bulletShotDelay.runtimeValue -= delayToDecrease;
        bulletMultiple.runtimeValue += additionalBullet;

        StartCoroutine(FireBullet(bulletMultiple.runtimeValue));
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