using UnityEngine;

public class SettingButton : MonoBehaviour
{
    public void OnPressSettingButton()
    {
        UIManager.Instance.OpenSetting();
    }
}
