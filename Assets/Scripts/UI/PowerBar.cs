using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    public Slider slider;

    public void SetPower(int power)
    {
        slider.value = power;
    }

    public void SetMinMaxPower(int min, int max)
    {
        slider.minValue = min;
        slider.maxValue = max;
        SetPower(min);
    }
    public int GetPower()
    {
        return (int) slider.value;
    }
}
