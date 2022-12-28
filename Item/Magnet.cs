using System.Collections;
using UnityEngine;

public class Magnet : MonoBehaviour, ICollectable, IPooledObject
{
    #region Field
    [Header("Magnet")]
    [SerializeField] StringVariable itemTag;
    [SerializeField] GameEvent OnPlayerGetMagnet;

    [Header("Magnet Status")]
    [SerializeField] FloatVariable magnetPullingPower;
    [SerializeField] FloatVariable magnetDuration;
    [SerializeField] FloatVariable itemDetectRange;
    [SerializeField] FloatVariable magnetElapsedTime;
    bool isMagnetOn;

    [Header("Other Setting")]
    [SerializeField] Vector3Variable playerPosition;
    [SerializeField] SpriteRenderer spriteRenderer;
    #endregion

    #region Method
    void ActivateMagnet()
    {
        isMagnetOn = true;
        SetMagnetPosition();
        StartCoroutine(MagnetEffect());
        StartCoroutine(MagnetTimeCheck());
    }

    IEnumerator MagnetEffect()
    {
        while (true)
        {
            transform.position = playerPosition.runtimeValue;

            Collider2D[] itemInRange = Physics2D.OverlapCircleAll(playerPosition.runtimeValue, itemDetectRange.runtimeValue, 1 << LayerMask.NameToLayer("Item"));
            if (itemInRange.Length > 0)
            {
                for (int i = 0; i < itemInRange.Length; i++)
                {
                    itemInRange[i].transform.position = Vector3.Lerp(itemInRange[i].transform.position, playerPosition.runtimeValue, magnetPullingPower.runtimeValue * Time.deltaTime);
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator MagnetTimeCheck()
    {
        while (true)
        {
            magnetElapsedTime.runtimeValue += Time.deltaTime;

            if (magnetElapsedTime.runtimeValue >= magnetDuration.runtimeValue)
            {
                transform.parent = null;
                transform.SetAsLastSibling();
                StopAllCoroutines();
                isMagnetOn = false;

                ObjectPooler.Instance.InsertToPool("Magnet", gameObject);
            }

            yield return null;
        }
    }

    void SetMagnetPosition()
    {
        transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
        transform.SetAsLastSibling();
    }
    
    public void Get()
    {
        if (!isMagnetOn)
            ActivateMagnet();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;

        OnPlayerGetMagnet.Raise();
    }

    public void OnObjectSpawn() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
    }

    public void OnObjectInsert() 
    {
        magnetElapsedTime.runtimeValue = 0f;
    }
    #endregion
}