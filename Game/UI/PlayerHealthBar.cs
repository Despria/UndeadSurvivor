using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : GameEventListener
{
    [Header("Value To Check")]
    [SerializeField] FloatVariable playerHP;

    [Header("UI components")]
    [SerializeField] Slider slider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;

    #region Unity Event
    void Start()
    {
        Response.AddListener(DeActivate);
        SetMaxHealth(playerHP.runtimeValue);
    }

    void Update()
    {
        SetHealth(playerHP.runtimeValue);
    }
    #endregion

    #region Method
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void DeActivate()
    {
        SetActive(false);
    }
    #endregion
}
