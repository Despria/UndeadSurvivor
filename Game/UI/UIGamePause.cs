using UnityEngine;

public class UIGamePause : MonoBehaviour
{
    public void SetGamePause()
    {
        Time.timeScale = 0f;
    }

    public void OnPressResumeButton()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void OnPressOptionButton()
    {
        UIManager.Instance.OpenSetting();
    }

    public void OnPressExitButton()
    {
        Time.timeScale = 1f;
        LoadSceneManager.LoadScene(SceneState.Title);
        SoundManager.Instance.PauseSound();

        gameObject.SetActive(false);
    }
}
