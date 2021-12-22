using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider.minValue = 1f;
    }

    public void SetCharge(float charge)
    {
        slider.value = charge;
    }

    public void SetMaxCharge(int max)
    {
        slider.maxValue = max;
    }
    public int GetCharge()
    {
        return (int) slider.value;
    }
}
