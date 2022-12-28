using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningShovel : GameEventListener
{
    #region Field
    [Header("Shovel")]
    [SerializeField] GameObject shovelPrefab;
    [SerializeField] FloatVariable shovelDistanceFromPlayer;
    [SerializeField] List<Transform> shovels;

    [Header("Skill Status")]
    [SerializeField] FloatVariable shovelDamage;
    [SerializeField] FloatVariable shovelSpeed;
    [SerializeField] IntVariable shovelNumber;

    [Header("Other Setting")]
    [SerializeField] Vector3Variable playerPosition;
    [SerializeField] Transform locater;
    #endregion

    #region Unity Event
    new void OnEnable()
    {
        base.OnEnable();
        transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
        transform.SetAsLastSibling();
        transform.position = new Vector3(playerPosition.runtimeValue.x, playerPosition.runtimeValue.y, 0f);

        shovelNumber.runtimeValue = shovels.Count;

        SetShovelsTransform();
    }

    void Start()
    {
        Response.AddListener(DeActivate);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, shovelSpeed.runtimeValue * Time.deltaTime));
    }
    #endregion

    #region Method
    public void SetShovelsTransform()
    {
        List<Vector3> points = new List<Vector3>();
        Vector3 point = Vector3.zero;

        /// 정n각형의 내각의 합은 180 * (n - 2)
        /// 정n각형의 각 내각은 180 * (n - 2) / n
        int shovelDegree = 180 * (shovels.Count - 2) / shovels.Count;

        for (int i = 0; i < shovels.Count; i++)
        {
            locater.Rotate(new Vector3(0f, 0f, 180 - shovelDegree));
            locater.Translate(Vector3.up * shovelDistanceFromPlayer.runtimeValue);
            shovels[i].position = locater.position;

            points.Add(locater.position);
            point += points[i];
        }

        Vector3 shovelCenter = point / shovels.Count;
        Vector3 playerCenter = playerPosition.runtimeValue;

        for (int i = 0; i < shovels.Count; i++)
        {
            shovels[i].position += new Vector3(playerCenter.x - shovelCenter.x, playerCenter.y - shovelCenter.y, 0f);            
            
            float degree = Mathf.Atan2(playerCenter.y - shovels[i].position.y, playerCenter.x - shovels[i].position.x) * Mathf.Rad2Deg;
            shovels[i].rotation = Quaternion.Euler(new Vector3(0f, 0f, degree + 90));
        }
    }

    public void SetShovelNumber(int shovelNumber)
    {
        if (shovels.Count >= shovelNumber)
            return;

        while (shovels.Count < shovelNumber)
        {
            GameObject newShovel = ObjectPooler.Instance.SpawnFromPool("Shovel", transform.position, Quaternion.identity);

            shovels.Add(newShovel.transform);
            newShovel.transform.SetParent(transform);
            newShovel.transform.SetAsLastSibling();
        }

        shovelDistanceFromPlayer.runtimeValue -= 0.2f;
        this.shovelNumber.runtimeValue = shovelNumber;

        SetShovelsTransform();
    }

    public void SetShovelStatus(float damageToAdd, float speedToAdd)
    {
        shovelDamage.runtimeValue += damageToAdd;
        shovelSpeed.runtimeValue += speedToAdd;
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