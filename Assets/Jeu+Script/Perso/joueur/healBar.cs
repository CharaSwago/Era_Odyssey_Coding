using UnityEngine;
using UnityEngine.UI;

public class healBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gardient;
    public Image fill;

    //La barre de vie au max
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gardient.Evaluate(1f);
    }

    //La barre de vie
    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gardient.Evaluate(slider.normalizedValue);
    }
}
