using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// LoadScene에서 Scene 로딩을 담당하는 LoadSceneManager 스크립트.
/// </summary>
 
/// Loading Scene - 베르 (https://wergia.tistory.com/183) 

public class LoadSceneManager : MonoBehaviour
{
    #region Field
    [SerializeField] LoadingBar loadingBar;
    [SerializeField] TipText tipText;
    AsyncOperation operation;

    public static int nextScene;
    #endregion

    #region Unity Event
    void Start()
    {
        tipText.SetTipText();
        StartCoroutine(LoadSceneAsync());
    }
    #endregion

    #region Method
    /// <summary>
    /// Scene을 전환하는 처리를 담당.<br/>
    /// 로딩용 Scene을 호출하고, 다시 매개변수에 해당하는 Scene으로 이동함.
    /// </summary>
    /// <param name="sceneState"></param>
    public static void LoadScene(SceneState sceneState)
    {
        nextScene = (int)sceneState;

        // Build Setting의 LoadScene의 인덱스가 0
        SceneManager.LoadScene((int)SceneState.Load);
    }

    IEnumerator LoadSceneAsync()
    {
        operation = SceneManager.LoadSceneAsync(nextScene);
        /// allowSceneActivation을 false로 하여 Scene 로딩을 90퍼센트 수준에서 멈추도록 함
        /// allowSceneActivation이 true로 변경될 시에만 나머지 로딩을 끝마치게 됨
        operation.allowSceneActivation = false;

        float timer = 0.0f;
        while (!operation.isDone)
        {
            yield return null;

            /// 로딩 진행 상황이 90퍼센트에 도달할 때까지는 로딩을 그대로 진행
            if (operation.progress < 0.9f)
            {
                loadingBar.Slider.value = operation.progress;
            }

            /// 로딩 진행 상황이 90퍼센트에 도달했을 경우,
            /// 일부러 짧은 페이크 로딩 시간을 발생시켜 대기하게 함
            else
            {
                timer += Time.unscaledDeltaTime;
                loadingBar.Slider.value = Mathf.Lerp(0.9f, 1f, timer);

                /// 페이크 로딩 시간도 지나 로딩바가 전부 찼을 경우,
                /// allowSceneActivation을 true로 전환하여 Scene을 완전히 전환시키고 코루틴 종료
                if (loadingBar.Slider.value >= 1f)
                {
                    operation.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
    #endregion
}