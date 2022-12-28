using UnityEngine;

public class Shovel : MonoBehaviour, IPooledObject
{
    [SerializeField] FloatVariable shovelDamage;

    #region Unity Event
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            collision.gameObject.GetComponent<Monster>().GetDamage(shovelDamage.runtimeValue);
        }
    }
    #endregion

    #region Method
    public void OnObjectInsert() { }

    public void OnObjectSpawn() { }
    #endregion
}