using UnityEngine;
using TMPro;

/// <summary>
/// 한 개의 버튼이 있는 팝업창 UI
/// </summary>
public class UIPopUpOneButton : MonoBehaviour
{
    #region Field
    [SerializeField] TextMeshProUGUI titleString;
    [SerializeField] TextMeshProUGUI uiContextString;
    [SerializeField] TextMeshProUGUI buttonString;

    ButtonDelegate buttonDelegate;
    #endregion

    #region Method
    public void SetPopUpOneButton(string title, string uiContext, string button, 
                                  ButtonDelegate buttonDelegate = null)
    {
        titleString.text = title;
        uiContextString.text = uiContext;
        buttonString.text = button;
        this.buttonDelegate = buttonDelegate;
    }

    public void OnPressButton()
    {
        if (buttonDelegate != null)
        {
            buttonDelegate();
        }
        else
        {
            UIManager.Instance.ClosePopUp(gameObject);
        }
    }
    #endregion
}