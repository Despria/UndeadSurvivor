using UnityEngine;
using TMPro;
using System.Text;

public class LevelText : GameEventListener
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] IntVariable playerLevel;
    StringBuilder stringBuilder;

    #region Unity Event
    void Awake()
    {
        levelText = GetComponent<TextMeshProUGUI>();
        stringBuilder = new StringBuilder();
        SetLevelText();
    }
    void Start()
    {
        Response.AddListener(SetLevelText);
    }
    #endregion

    #region Method
    public void SetLevelText()
    {
        stringBuilder.Clear();
        levelText.text = stringBuilder.AppendFormat("Lv.{0}", playerLevel.runtimeValue).ToString();
    }
    #endregion
}