using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnergyBar : MonoBehaviour
{
    public Slider energybarSlider;

    public void GiveFullEnergy(float energy)
    {
        energybarSlider.maxValue = energy;
        energybarSlider.value = energy;
    }

    public void SetEnergy(float energy)
    {
        energybarSlider.value = energy;
    }
}
