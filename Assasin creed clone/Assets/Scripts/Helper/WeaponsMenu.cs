using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsMenu : MonoBehaviour
{
    public GameObject weaponsMenuUI;
    public bool weaponsMenuActive = false;
    public GameObject mainCamera;

    [Header("Weapons")]
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject weapon4;
    public GameObject weapon4StockUI;

    [Header("Rations")]
    public Inventory inventory;

    [Header("Menus")]
    public GameObject playerUI;
    public GameObject miniMapCanvas;
    public GameObject currentmenuUI;

    private void Update()
    {
        if (weaponsMenuActive == true)
        {
            playerUI.SetActive(false);
            miniMapCanvas.SetActive(false);
            currentmenuUI.SetActive(false);
        }

        if (weaponsMenuActive == false)
        {
            playerUI.SetActive(true);
            miniMapCanvas.SetActive(true);
            currentmenuUI.SetActive(true);
        }

        WeaponsCheck();

        if (Input.GetKeyDown(KeyCode.Tab) && weaponsMenuActive == false)
        {
            //open weapon menu
            weaponsMenuUI.SetActive(true);
            weaponsMenuActive = true;
            Time.timeScale = 0;
            mainCamera.GetComponent<CameraController>().enabled = false;
        }

        else if (Input.GetKeyDown(KeyCode.Tab) && weaponsMenuActive == true)
        {
            //close weapon menu
            weaponsMenuUI.SetActive(false);
            weaponsMenuActive = false;
            Time.timeScale = 1;
            mainCamera.GetComponent<CameraController>().enabled = true;
        }

       

    }

    void WeaponsCheck()
    {
        if (inventory.isWeapon1Picked == true)
        {
            weapon1.SetActive(true);
        }

        if (inventory.isWeapon2Picked == true)
        {
            weapon2.SetActive(true);
        }

        if (inventory.isWeapon3Picked == true)
        {
            weapon3.SetActive(true);
        }

        if (inventory.isWeapon4Picked == true)
        {
            weapon4.SetActive(true);
            weapon4StockUI.SetActive(true);
        }
    }
}
