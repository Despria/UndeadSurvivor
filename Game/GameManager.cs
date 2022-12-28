using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Time Management")]
    [SerializeField] FloatVariable gameElapsedTime;
    [SerializeField] BoolVariable isTimeOver;
    bool isGameFinished;

    [Header("Game Event")]
    [SerializeField] GameEvent OnPlayerWin;
    [SerializeField] GameEvent OnMonsterEnhanceInEveryMinute;

    WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);

    #region Singleton
    public static GameManager Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;

        SoundManager.Instance.PlayLoopSound(SoundContainer.Instance.Clips[(int)SoundState.Game]);
        GameSceneUI.Instance.SetGameUI();

        StartCoroutine(GameGetReady());
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android) 
        { 
            if (Input.GetKeyDown(KeyCode.Escape)) 
            {
                GameSceneUI.Instance.OpenGamePause();
            }
        }
    }
    #endregion
    public void GameFinished(bool isGameWin)
    {
        StartCoroutine(OnGameFinished(isGameWin));
    }

    IEnumerator GameGetReady()
    {
        int count = 3;

        var spawner = GameObject.FindGameObjectWithTag("ObjectSpawner");
        spawner.SetActive(false);

        while (count >= 0)
        {
            GameSceneUI.Instance.SetCountdownText(count.ToString());
            yield return new WaitForSeconds(1f);
            count--;

            if (count < 0)
            {
                GameSceneUI.Instance.SetCountdownText("Start!!");
                GameSceneUI.Instance.OpenSkillSelect();

                if (GameSceneUI.Instance.IsCountdownActive())
                    GameSceneUI.Instance.SetCountdownActive(false);

                GameSceneUI.Instance.SetTimerActive(true);
                spawner.SetActive(true);
                StartCoroutine(CheckGameFinish());
                StartCoroutine(MonsterEnhance());

                yield break;
            }
        }
    }

    IEnumerator MonsterEnhance()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f);
            OnMonsterEnhanceInEveryMinute.Raise(); 
        }
    }

    IEnumerator CheckGameFinish()
    {
        while (true)
        {
            if (isTimeOver.runtimeValue && !isGameFinished)
            {
                StopAllCoroutines();
                OnPlayerWin.Raise();
                GameFinished(true);
                yield break;
            }

            yield return waitForSeconds;
        }
    }

    IEnumerator OnGameFinished(bool isGameWin)
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
        isGameFinished = true;

        GameSceneUI.Instance.OpenGameResult(isGameWin);
    }
}