using System.Collections;
using UnityEngine;

public class CameraShake : GameEventListener
{
    #region Field
    [SerializeField] float shakeTime;
    [SerializeField] float shakeSpeed;
    #endregion

    #region Unity Event
    void Start()
    {
        Response.AddListener(Activate);
    }
    #endregion

    #region Method
    public void Activate()
    {
        StartCoroutine(ShakeCamera());
    }

    IEnumerator ShakeCamera()
    {
        Vector2 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < shakeTime)
        {
            Vector2 randomPoint = startPosition + (Random.insideUnitCircle * shakeSpeed);
            transform.position = Vector3.Lerp(transform.position, randomPoint, shakeSpeed * Time.deltaTime);

            yield return null;

            elapsedTime += Time.deltaTime;
        }
    }
    #endregion
}
