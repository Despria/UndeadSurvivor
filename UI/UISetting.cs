using TMPro;
using UnityEngine;

/// <summary>
/// 설정창 UI.
/// </summary>
public class UISetting : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI volumeText;

    public void SetVolume(float volume)
    {
        if (volume == 0)
        {
            SoundManager.Instance.SetVolume(-80f);
            volumeText.text = "X";
            return;
        }
        SoundManager.Instance.SetVolume(volume);
        volumeText.text = string.Format("{0:###}", (volume * 100));
    }

    public void CloseButton()
    {
        UIManager.Instance.ClosePopUp(gameObject);
    }
}