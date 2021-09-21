using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLife : MonoBehaviour
{
    public Slider slider;
    protected int maxHP = 100;
    protected int currHP;

    // Start is called before the first frame update
    void Start()
    {
        SetMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMaxHealth()
    {
        slider.maxValue = maxHP;
        currHP = maxHP;
        slider.value = currHP;
    }

    public void SetHealth()
    {
        slider.value = currHP;
    }
}
