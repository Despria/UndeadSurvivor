using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;

    public Slider Slider { get { return slider; } }
    public Image Fill { get { return fill; } }

    void UpdateLoadingBar()
    {
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLoadingBar();
    }
}
