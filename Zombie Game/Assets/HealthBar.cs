using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    private void Update()
    {
        if (slider.value > 3 * slider.maxValue / 4) // Health > 75%
        {
            fill.color = Color.green;
        }
        else if (slider.value > slider.maxValue / 3) //Health > 33%
        {
            fill.color = Color.yellow;
        }
        else //Health < 33%
        {
            fill.color = Color.red;
        }
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void setActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
