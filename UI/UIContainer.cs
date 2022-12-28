using UnityEngine;

public enum UI
{
    PopupOneButton = 0,
    PopupTwoButton,
    PopupLine,
    Setting
}

[CreateAssetMenu(fileName = "UI Container", menuName = "Scriptable Objects/Singleton Asset/UI Container", order = 8)]
public class UIContainer : SingletonScriptableObject<UIContainer>
{
    [Header("Order as UI Enum")]
    [Tooltip("PopUpOneButton = 0\n" +
             "PopUpTwoButton = 1\n" +
             "PopUpLine = 2\n" +
             "Setting = 3")]
    public GameObject[] UI;
}