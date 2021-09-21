using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider slider;
    public Text maxHP;
    public Text HP;

    public void setMaxHealth(int maxHealth)
    {
        maxHP.text = maxHealth.ToString();
        slider.maxValue = maxHealth;
    }

    public void SetHealth(int health)
    {
        HP.text = health.ToString();
        slider.value = health;
    }


}
