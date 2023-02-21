using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float grenadeTimer = 3f;
    float countDown;
    bool hasExploded = false;
    public float GiveDamage=120f;
    public int Radius = 10;
    public GameObject ExplosionEffect;
    private void Start()
    {
        countDown = grenadeTimer;
    }
    private void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }

    }
    void Explode()
    {
        Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        Collider[] colliders = Physics.OverlapSphere(transform.position, Radius);
        foreach(Collider nearbyObjects in colliders)
        {
            Object obj = nearbyObjects.GetComponent<Object>();
            if(obj!=null)
            {
                obj.objectHitDamage(GiveDamage);
            }
        }
        Debug.Log(" Grenade Exploded");
        Destroy(gameObject);
    }
}
