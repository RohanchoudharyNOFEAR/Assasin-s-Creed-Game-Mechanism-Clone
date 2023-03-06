using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistFight : MonoBehaviour
{
    public float Timer = 0f;
    private int FistFightVal;
    public Animator anim;
    public Transform attackArea;
    public float giveDamage = 10f;
    public float attackRadius;
    public LayerMask knightLayer;
    public PlayerController playerCon;
    public Inventory Inventory;

    [SerializeField] Transform LeftHandPunch;
    [SerializeField] Transform RightHandPunch;
    [SerializeField] Transform LeftLegKick;


    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            Timer += Time.deltaTime;
        }
        else
        {
            Debug.Log("Fist FighmodeoN");
            playerCon.movementSpeed = 3f;
            anim.SetBool("FistFightActive", true);
            Timer = 0f;
        }
        if (Timer > 5f)
        {
            Debug.Log("Fist Fight mode OFF");
            playerCon.movementSpeed = 5f;
            anim.SetBool("FistFightActive", false);
            Inventory.fistFightMode = false;
            Timer = 0;
            this.gameObject.GetComponent<FistFight>().enabled = false;
        }
        FistfightModes();
    }

    void FistfightModes()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FistFightVal = Random.Range(1, 6);
            if (FistFightVal == 1)
            {
                //Attack
                attackArea = LeftHandPunch;
                attackRadius = 0.5f;
                Attack();
                //AniMation
                StartCoroutine(SingleFist());
            }
            if (FistFightVal == 2)
            {
                attackArea = RightHandPunch;
                attackRadius = 0.6f;
                Attack();

                StartCoroutine(Doublefist());
            }
            if (FistFightVal == 3)
            {
                attackArea = LeftHandPunch;
                attackArea = LeftLegKick;
                attackRadius = 0.7f;
                Attack();

                StartCoroutine(FirstFistkick());
            }
            if (FistFightVal == 4)
            {
                attackArea = LeftLegKick;
                attackRadius = 0.7f;
                Attack();

                StartCoroutine(KickCombo());
            }
            if (FistFightVal == 5)
            {
                attackArea = LeftLegKick;
                attackRadius = 0.9f;
                Attack();

                StartCoroutine(Leftkick());
            }

        }

    }

    IEnumerator SingleFist()
    {
        anim.SetBool("SingleFist", true);

        playerCon.movementSpeed = 0f;
        anim.SetFloat("movementValue", 0);

        yield return new WaitForSeconds(0.68f);
        anim.SetBool("SingleFist", false);
        playerCon.movementSpeed = 5f;
        anim.SetFloat("movementValue", 0);
    }
    IEnumerator Doublefist()
    {
        playerCon.movementSpeed = 0f;
        anim.SetFloat("movementValue", 0);

        anim.SetBool("DoubleFist", true);
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("DoubleFist", false);
        playerCon.movementSpeed = 5f;
        anim.SetFloat("movementValue", 0);
    }
    IEnumerator FirstFistkick()
    {
        playerCon.movementSpeed = 0f;
        anim.SetFloat("movementValue", 0);

        anim.SetBool("FirstFistKick", true);
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("FirstFistKick", false);
        playerCon.movementSpeed = 5f;
        anim.SetFloat("movementValue", 0);
    }
    IEnumerator KickCombo()
    {
        playerCon.movementSpeed = 0f;
        anim.SetFloat("movementValue", 0);

        anim.SetBool("KickCombo", true);
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("KickCombo", false);
        playerCon.movementSpeed = 5f;
        anim.SetFloat("movementValue", 0);
    }
    IEnumerator Leftkick()
    {
        playerCon.movementSpeed = 0f;
        anim.SetFloat("movementValue", 0);

        anim.SetBool("LeftKick", true);
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("LeftKick", false);
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
        if(attackArea==null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackArea.position,attackRadius);
    }
}

