using UnityEngine;

public class PauseButton : MonoBehaviour
{
    #region Field
    #endregion

    #region Method
    public void OnPressPauseButton()
    {
        GameSceneUI.Instance.OpenGamePause();
    }
    #endregion

    #region Unity Event
    #endregion
}
