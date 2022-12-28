using UnityEngine;
using TMPro;

/// <summary>
/// 두 개의 버튼이 있는 팝업창 UI
/// </summary>
public class UIPopUpTwoButton : MonoBehaviour
{
    #region Field
    [SerializeField] TextMeshProUGUI titleString;
    [SerializeField] TextMeshProUGUI uiContextString;
    [SerializeField] TextMeshProUGUI leftButtonString;
    [SerializeField] TextMeshProUGUI rightButtonString;

    ButtonDelegate leftButtonDelegate;
    ButtonDelegate rightButtonDelegate;
    #endregion

    #region Method
    public void SetPopUpTwoButton(string title, string uiContext, 
                                  string leftButton, string rightButton, 
                                  ButtonDelegate leftButtonDelegate = null, 
                                  ButtonDelegate rightButtonDelegate = null)
    {
        titleString.text = title;
        uiContextString.text = uiContext;
        leftButtonString.text = leftButton;
        this.leftButtonDelegate = leftButtonDelegate;
        rightButtonString.text = rightButton;
        this.rightButtonDelegate = rightButtonDelegate;
    }

    public void OnPressLeftButton()
    {
        if (leftButtonDelegate != null)
        {
            leftButtonDelegate();
        }
        else
        {
            UIManager.Instance.ClosePopUp(gameObject);
        }
    }

    public void OnPressRightButton()
    {
        if (rightButtonDelegate != null)
        {
            rightButtonDelegate();
        }
        else
        {
            UIManager.Instance.ClosePopUp(gameObject);
        }
    }
    #endregion
}
