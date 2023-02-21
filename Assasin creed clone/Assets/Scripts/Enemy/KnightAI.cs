using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAI : MonoBehaviour
{
    public float Maxhealth = 120f;
    public float curHealth;
    private void Start()
    {
        curHealth = Maxhealth;
    }
    public void TakeDamage(float amount)
    {
        curHealth -= amount;
        if (curHealth <= 0f)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
