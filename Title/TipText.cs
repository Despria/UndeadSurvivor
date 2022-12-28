using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class TipText : MonoBehaviour
{
    [Header("Text Object")]
    [SerializeField] TextMeshProUGUI tipText;

    [Header("Tips List")]
    [SerializeField] List<StringVariable> tips;

    public void SetTipText()
    {
        int random = Random.Range(0, tips.Count);
        tipText.text = string.Format("팁: {0}", tips[random].runtimeValue);
    }
}
