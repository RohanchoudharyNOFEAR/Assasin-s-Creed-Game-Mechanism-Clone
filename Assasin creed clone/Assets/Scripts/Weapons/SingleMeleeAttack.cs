using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMeleeAttack : MonoBehaviour
{
   
    private int SingleMelleValue;
    public Animator anim;
    public Transform attackArea;
    public float giveDamage = 10f;
    public float attackRadius;
    public LayerMask knightLayer;
    public PlayerController playerCon;

   

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            anim.SetBool("SingleHandAttackActive", true);
        }
       
        SingleMeleeModes();
    }

    void SingleMeleeModes()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SingleMelleValue = Random.Range(1, 6);
            if (SingleMelleValue == 1)
            {
                //Attack
              
                Attack();
                //AniMation
                StartCoroutine(SingleAttack1());
            }
            if (SingleMelleValue == 2)
            {
              
                Attack();

                StartCoroutine(SingleAttack2());
            }
            if (SingleMelleValue == 3)
            {
               
                Attack();

                StartCoroutine(SingleAttack3());
            }
            if (SingleMelleValue == 4)
            {
              
                Attack();

                StartCoroutine(SingleAttack4());
            }
            if (SingleMelleValue == 5)
            {
               
                Attack();

                StartCoroutine(SingleAttack5());
            }

        }

    }

    IEnumerator SingleAttack1()
    {
        anim.SetBool("SingleAttack1", true);

        playerCon.movementSpeed = 0f;
        anim.SetFloat("movementValue", 0);

        yield return new WaitForSeconds(0.2f);
        anim.SetBool("SingleAttack1", false);
        playerCon.movementSpeed = 5f;
        anim.SetFloat("movementValue", 0);
    }
    IEnumerator SingleAttack2()
    {
        playerCon.movementSpeed = 0f;
        anim.SetFloat("movementValue", 0);

        anim.SetBool("SingleAttack2", true);
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("SingleAttack2", false);
        playerCon.movementSpeed = 5f;
        anim.SetFloat("movementValue", 0);
    }
    IEnumerator SingleAttack3()
    {
        playerCon.movementSpeed = 0f;
        anim.SetFloat("movementValue", 0);

        anim.SetBool("SingleAttack3", true);
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("SingleAttack3", false);
        playerCon.movementSpeed = 5f;
        anim.SetFloat("movementValue", 0);
    }
    IEnumerator SingleAttack4()
    {
        playerCon.movementSpeed = 0f;
        anim.SetFloat("movementValue", 0);

        anim.SetBool("SingleAttack4", true);
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("SingleAttack4", false);
        playerCon.movementSpeed = 5f;
        anim.SetFloat("movementValue", 0);
    }
    IEnumerator SingleAttack5()
    {
        playerCon.movementSpeed = 0f;
        anim.SetFloat("movementValue", 0);

        anim.SetBool("SingleAttack5", true);
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("SingleAttack5", false);
        playerCon.movementSpeed = 5f;
        anim.SetFloat("movementValue", 0);
    }

    void Attack()
    {
        Collider[] hitKnight = Physics.OverlapSphere(attackArea.position, attackRadius, knightLayer);
        foreach (Collider knight in hitKnight)
        {
            KnightAI knightAI = knight.GetComponent<KnightAI>();

            if (knightAI != null)
            {
                knightAI.TakeDamage(giveDamage);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackArea == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackArea.position, attackRadius);
    }
}
