using GoogleMobileAds.Api;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Title : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tapToPlayText;
    
    void Start()
    {
        SoundManager.Instance.PlayLoopSound(SoundContainer.Instance.Clips[(int)SoundState.Title]);
        
        MobileAds.Initialize((initStatus) => { });
        AdMobBanner.Instance.SetTestDeviceID();
        AdMobBanner.Instance.InitAd();
        AdMobBanner.Instance.ShowAd();

        StartCoroutine(BlinkText(tapToPlayText));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                SoundManager.Instance.PauseSound();
                AdMobBanner.Instance.HideAd();
                LoadSceneManager.LoadScene(SceneState.Game);
            }
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UIManager.Instance.OpenPopUpTwoButton("게임 종료", "게임을 종료하시겠습니까?", "예", "아니오",
                                                     () => Application.Quit());
            }
        }
    }

    IEnumerator BlinkText(TextMeshProUGUI text)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            tapToPlayText.gameObject.SetActive(!tapToPlayText.gameObject.activeSelf);
        }
    }
}