using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    [SerializeField] FloatVariable bulletDamage;
    [SerializeField] FloatVariable bulletSpeed;

    [SerializeField] Vector3 direction;
    [SerializeField] float bulletRemainingTime;

    #region Unity Event
    void Update()
    {
        transform.position += bulletSpeed.runtimeValue * Time.deltaTime * direction;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            collision.gameObject.GetComponent<Monster>().GetDamage(bulletDamage.runtimeValue);
            ObjectPooler.Instance.InsertToPool("Bullet", gameObject);
        }
    }
    #endregion

    #region Method
    IEnumerator ReturnToPool()
    {
        yield return new WaitForSeconds(bulletRemainingTime);
        ObjectPooler.Instance.InsertToPool("Bullet", gameObject);
    }

    public void SetBullet(Vector3 direction)
    {
        float degree = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, degree + 90));

        this.direction = direction.normalized;
    }

    public void OnObjectSpawn()
    {
        StartCoroutine(ReturnToPool());
    }

    public void OnObjectInsert() { }
    #endregion
}