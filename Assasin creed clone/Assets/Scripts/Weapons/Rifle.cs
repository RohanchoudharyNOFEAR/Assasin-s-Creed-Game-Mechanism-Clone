using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Rifle Things")]
    public Transform shootingArea;
    public float giveDamage = 10f;
    public float shootingRange = 100f;
    public Animator Animator;
    public bool IsMoving;
    public PlayerController playerCon;
    public GameObject Crosshair;

    [Header("Rifle Ammunition and reloading")]
    private int maximumAmmunition = 1;
    public int presentAmmunition;
    public int mag;
    public float ReloadingTime;
    private bool _setReloading;

    private void Start()
    {
        presentAmmunition = maximumAmmunition;
    }


    private void Update()
    {
        if(Animator.GetFloat("movementValue")>0.001f)
        {
            IsMoving = true;
        }
        else if (Animator.GetFloat("movementValue") < 0.0999999f)
        {
            IsMoving = false;
        }

        if (_setReloading)
        {
            return;
        }
        if(presentAmmunition<=0 && mag >0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButtonDown(0)&&IsMoving ==false)
        {
            Animator.SetBool("RifleActive", true);
            Animator.SetBool("Shooting", true);
            Shoot();
        }
        else if(!Input.GetMouseButtonDown(0))
        {
            Animator.SetBool("Shooting", false);
        }
        if(Input.GetMouseButton(1))
        {
            Animator.SetBool("RifleAim", true);
           // Crosshair.SetActive(true);
        }
        else if (!Input.GetMouseButton(1))
        {
            Animator.SetBool("RifleAim", false);
          //  Crosshair.SetActive(false);
        }
    }

    void Shoot()
    {

        if (mag <= 0)
        {
            // show out U 
            return;
        }
        presentAmmunition--;
        if (presentAmmunition == 0)
        {
            mag--;
        }


        RaycastHit hitInfo;
        if (Physics.Raycast(shootingArea.position, shootingArea.forward, out hitInfo, shootingRange))
        {
            Debug.Log(hitInfo.transform.name);
            KnightAI knightAI = hitInfo.transform.GetComponent<KnightAI>();
            if (knightAI != null)
            {
                knightAI.TakeDamage(giveDamage);
            }

        }
    }

    IEnumerator Reload()
    {
        _setReloading = true;
        Animator.SetFloat("movementValue", 0);
        playerCon.movementSpeed = 0;
        Animator.SetBool("ReloadRifle", true);
        yield return new WaitForSeconds(ReloadingTime);
       
        presentAmmunition = maximumAmmunition;
        Animator.SetBool("ReloadRifle", false);
        _setReloading = false;
        Animator.SetFloat("movementValue", 0);
        playerCon.movementSpeed = 5;
    }

}
