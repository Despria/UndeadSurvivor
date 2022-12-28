using UnityEngine;
using UnityEngine.UI;

public class PlayerExpBar : GameEventListener
{
    [Header("Value To Check")]
    [SerializeField] FloatVariable playerMaximumExp;
    [SerializeField] FloatVariable playerExp;

    [Header("UI components")]
    [SerializeField] Slider slider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;

    #region Unity Event
    void Start()
    {
        Response.AddListener(SetMaxExp);
        SetMaxExp();
    }

    void Update()
    {
        SetExp(playerExp.runtimeValue);
    }
    #endregion

    #region Method
    public void SetMaxExp()
    {
        slider.maxValue = playerMaximumExp.runtimeValue;
        slider.value = 0f;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetExp(float exp)
    {
        slider.value = exp;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
    #endregion
}
