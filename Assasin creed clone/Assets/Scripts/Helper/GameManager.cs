using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public int NumberofGrenades;
    public int NumberOfHealth;
    public int NumberOfEnergy;

    [Header("Ammo & Mag")]
    public Rifle rifle;
    public Bazooka bazooka;
    public Text RifleAmmoText;
    public Text RifleMagText;
    public Text BazookaAmmoText;
    public Text BazookaMagText;


    [Header("Stocks")]
    public Text GrenadeStock1;
    public Text GrenadeStock2;
    public Text HealthStock;
    public Text EnergyStock;

    [Header("Health&Energy")]
    public GameObject healthSlot;
    public GameObject energySlot;


    private void Update()
    {
        //show Ammo & Mag for rifle and bazooka
        RifleAmmoText.text = "" + rifle.presentAmmunition;
        RifleMagText.text = "" + rifle.mag;

        BazookaAmmoText.text = "" + bazooka.presentAmmunition;
        BazookaMagText.text = "" + bazooka.mag;

        //show stock for greade health and energy
        GrenadeStock1.text = "" + NumberofGrenades;
        GrenadeStock2.text = "" + NumberofGrenades;
        HealthStock.text = "" + NumberOfHealth;
        EnergyStock.text = "" + NumberOfEnergy;

        if (NumberOfHealth > 0)
        {
            healthSlot.SetActive(true);
        }

        else if (NumberOfHealth <= 0)
        {
            healthSlot.SetActive(false);
        }

        if (NumberOfEnergy > 0)
        {
            energySlot.SetActive(true);
        }

        else if (NumberOfEnergy <= 0)
        {
            energySlot.SetActive(false);
        }

    }

}
