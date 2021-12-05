using UnityEngine.UI;
using UnityEngine;

public class ManaBar : MonoBehaviour
{
    public Slider slider;

    public void SetMana(int mana)
    {
        slider.value = mana;
    }

    public void SetMaxMana(int max)
    {
        slider.maxValue = max;
    }
}
