using UnityEngine;
using UnityEngine.UI;

public class ManaBarScript : MonoBehaviour
{
    public Slider slider;
    public Text maxMana;
    public Text mana;

    public void SetMaxMana(int max)
    {
        slider.maxValue = max;
        slider.value = max;
        maxMana.text = max.ToString();
        mana.text = max.ToString();
    }

    public void SetMana(int currMana)
    {
        slider.value = currMana;
        mana.text = currMana.ToString();
    }
}
