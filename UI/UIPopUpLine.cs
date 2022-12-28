using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// 일정 시간 동안만 정해진 문장을 표시하는 UI
/// </summary>
public class UIPopUpLine : MonoBehaviour
{
    #region Field
    [SerializeField] RectTransform panelTransform;
    [SerializeField] TextMeshProUGUI contextString;
    float popUpTime;
    #endregion

    #region Method
    public void SetPopUpLine(string text, Vector3 position, float time)
    {
        contextString.text = text;
        panelTransform.position = transform.position + position;
        popUpTime = time;

        StartCoroutine(PopUpDisappearTime());
    }

    IEnumerator PopUpDisappearTime()
    {
        yield return new WaitForSeconds(popUpTime);
        gameObject.SetActive(false);
    }
    #endregion
}