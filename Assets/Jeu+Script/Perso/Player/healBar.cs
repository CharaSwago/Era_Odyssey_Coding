using UnityEngine;
using UnityEngine.UI;

public class healBar : MonoBehaviour
{
    public Slider slider;

    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        UpdateFillColor();
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        UpdateFillColor();
    }

    private void UpdateFillColor()
    {
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
