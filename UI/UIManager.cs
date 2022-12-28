using System.Collections.Generic;
using UnityEngine;

public delegate void ButtonDelegate();

public class UIManager : DontDestroyOnLoadSingleton<UIManager>
{
    #region Field
    public Dictionary<UI, GameObject> UIDictionary;
    #endregion

    #region Method
    public void OpenPopUpOneButton(string title, string uiContext, string buttonText, ButtonDelegate buttonDelegate = null)
    {
        UIDictionary[UI.PopupOneButton].SetActive(true);
        UIDictionary[UI.PopupOneButton].transform.position = Vector3.zero;

        var popOne = UIDictionary[(int)UI.PopupOneButton].GetComponent<UIPopUpOneButton>();
        popOne.SetPopUpOneButton(title, uiContext, buttonText, buttonDelegate);
    }

    public void OpenPopUpTwoButton(string title, string uiContext, string leftButton, string rightButton, ButtonDelegate leftButtonDelegate = null, ButtonDelegate rightButtonDelegate = null)
    {
        UIDictionary[UI.PopupTwoButton].SetActive(true);
        UIDictionary[UI.PopupTwoButton].transform.position = Vector3.zero;

        var popTwo = UIDictionary[UI.PopupTwoButton].GetComponent<UIPopUpTwoButton>();
        popTwo.SetPopUpTwoButton(title, uiContext, leftButton, rightButton, leftButtonDelegate, rightButtonDelegate);
    }

    public void OpenPopUpLine(string text, Vector3 position, float time)
    {
        UIDictionary[UI.PopupLine].SetActive(true);

        var popLine = UIDictionary[UI.PopupLine].GetComponent<UIPopUpLine>();
        popLine.SetPopUpLine(text, position, time);
    }

    public void OpenSetting()
    {
        UIDictionary[UI.Setting].SetActive(true);
    }

    public void ClosePopUp(GameObject obj)
    {
        obj.SetActive(false);
    }
    #endregion

    new void Awake()
    {
        base.Awake();

        UIDictionary = new Dictionary<UI, GameObject>();
        for (int i = 0; i < UIContainer.Instance.UI.Length; i++)
        {
            UIDictionary.Add((UI)i, Instantiate(UIContainer.Instance.UI[i]));
            UIDictionary[(UI)i].transform.SetParent(transform);
            UIDictionary[(UI)i].SetActive(false);
        }
    }
}