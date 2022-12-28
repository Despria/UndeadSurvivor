using TMPro;
using UnityEngine;

public class GameSceneUI : MonoBehaviour
{
    [Header("Game UI PopUps")]
    [SerializeField] GameObject gamePause;
    [SerializeField] GameObject gameResult;
    [SerializeField] GameObject skillSelect;
    [SerializeField] GameObject gameTimer;
    [SerializeField] TextMeshProUGUI countdown;

    public static GameSceneUI Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    public void SetGameUI()
    {
        gamePause.SetActive(false);
        gameResult.SetActive(false);
        skillSelect.SetActive(false);
        gameTimer.SetActive(false);
        countdown.gameObject.SetActive(true);
    }

    public void SetTimerActive(bool active)
    {
        gameTimer.SetActive(active);
    }

    public void SetCountdownText(string text)
    {
        countdown.text = text;
    }

    public void SetCountdownActive(bool active)
    {
        countdown.gameObject.SetActive(active);
    }

    public bool IsCountdownActive()
    {
        if (countdown.gameObject.activeSelf) 
        {
            return true;
        }
        return false;
    }

    public void OpenGamePause()
    {
        gamePause.SetActive(true);
        gamePause.GetComponent<UIGamePause>().SetGamePause();
    }

    public void OpenGameResult(bool isGameWin)
    {
        gameResult.SetActive(true);
        gameResult.GetComponent<UIGameResult>()
                  .SetGameResult(isGameWin, () =>
                  {
                      SoundManager.Instance.PauseSound();
                      Time.timeScale = 1.0f;
                      LoadSceneManager.LoadScene(SceneState.Title);
                  });
    }

    public void OpenSkillSelect()
    {
        skillSelect.SetActive(true);
    }
}