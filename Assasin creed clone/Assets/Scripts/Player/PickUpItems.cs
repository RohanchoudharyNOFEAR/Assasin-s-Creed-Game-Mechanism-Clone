using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : MonoBehaviour
{
    [Header("Item Info")]
    public float itemRadius;
    public string ItemTag;
    private GameObject ItemToPick;
    

    [Header("Player Info")]
    public Transform player;
    public Inventory inventory;
    public GameManager GM;


    private void Start()
    {
        ItemToPick = GameObject.FindWithTag(ItemTag);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < itemRadius)
        {
            if(Input.GetKeyDown("f"))
            {
               // Missions.instance.Mission1 = true;
                if (ItemTag == "Sword")
                {
                     inventory.isWeapon1Picked = true;
                    Debug.Log("sword picked");
                }

                else if (ItemTag == "Rifle")
                {
                      inventory.isWeapon2Picked = true;
                    Debug.Log("riflepicked");
                }

                else if (ItemTag == "Bazooka")
                {
                     inventory.isWeapon3Picked = true;
                    Debug.Log("bazooka picked");
                }

                else if (ItemTag == "Grenade")
                {
                     GM.NumberofGrenades += 5;
                     inventory.isWeapon4Picked = true;
                    Debug.Log("grenade picked");
                }

                else if (ItemTag == "Health")
                {
                      GM.NumberOfHealth += 1;
                    Debug.Log("health picked");
                }

                else if (ItemTag == "Energy")
                {
                      GM.NumberOfEnergy += 1;
                    Debug.Log("energy picked");
                }

                ItemToPick.SetActive(false);
            }
        }
    }
}
