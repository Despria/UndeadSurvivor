using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 게임 결과창을 표시하는 UI 스크립트
/// </summary>

public class UIGameResult : MonoBehaviour
{
    #region Field
    [Header("UI Text")]
    [SerializeField] TextMeshProUGUI buttonString;

    [Header("UI Image")]
    [SerializeField] Image[] winImage;
    [SerializeField] Image[] loseImage;

    ButtonDelegate buttonDelegate;
    #endregion

    #region Method
    
    public void SetGameResult(bool isGameWin, ButtonDelegate buttonDelegate = null)
    {
        this.buttonDelegate = buttonDelegate;

        if (isGameWin)
        {
            for (int i = 0; i < loseImage.Length; i++) 
                loseImage[i].enabled = false;
            return;
        }
        
        for (int i = 0; i < winImage.Length; i++)
            winImage[i].enabled = false;
    }
    
    public void OnPressButton()
    {
        if (buttonDelegate != null)
        {
            buttonDelegate();
        }
        else
        {
            LoadSceneManager.LoadScene(SceneState.Title);
        }
    }
    #endregion
}
