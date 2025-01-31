using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Slider slider;
    [SerializeField] private Image fillImage;

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        slider.value = currentHealth / maxHealth;

        fillImage.color = GetHealthColor(slider.value);
    }

    private Color GetHealthColor(float healthPercentage)
    {
        if (healthPercentage > 0.5f)
        {
            // Green to yellow (fullHealthColor to midHealthColor)
            return Color.Lerp(Color.yellow, Color.green, (healthPercentage - 0.5f) * 2f);
        }
        else
        {
            // Yellow to orange (midHealthColor to lowHealthColor)
            return Color.Lerp(Color.red, Color.yellow, healthPercentage * 2f);
        }
    }
}
