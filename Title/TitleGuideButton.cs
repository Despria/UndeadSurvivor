using UnityEngine;

public class TitleGuideButton : MonoBehaviour
{
    [SerializeField] StringVariable guideTitle;
    [SerializeField] StringVariable guideContext;
    [SerializeField] StringVariable guideButton;
    
    public void OnPressGuideButton()
    {
        UIManager.Instance.OpenPopUpOneButton(guideTitle.runtimeValue,
                                              "10분간 언데드들의 공격을 피하며 생존하세요!\n" +
                                              "조이스틱을 통해 캐릭터를 움직일 수 있습니다.\n" +
                                              "준비된 3가지의 스킬들을 획득해가며 살아남으세요!",
                                              guideButton.runtimeValue,
                                              () => UIManager.Instance.ClosePopUp(UIManager.Instance.UIDictionary[UI.PopupOneButton]));
    }
}
