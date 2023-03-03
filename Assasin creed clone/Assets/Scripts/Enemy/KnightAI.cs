using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAI : MonoBehaviour
{
    [Header("Character Info")]
    public float Maxhealth = 120f;
    public float curHealth;
    public float RunningSpeed;
    private float _currentMovingSpeed;
    public float MovingSpeed;
    public float TurningSpeed = 300f;
    public float StopSpeed;
   // public float visionRadius;
   // public float attackRadius;
  //  public bool playerInvisionRadius;
   // public GameObject playerBody;
   // public LayerMask playerLayer;

    [Header("Destination var")]
    public Vector3 destination;
    public bool destinationReached;

    [Header("Knight AI")]
    public GameObject playerBody;
    public LayerMask playerLayer;
    public float visionRadius;
    public float attackRadius;
    public bool playerInvisionRadius;
    public bool playerInattackRadius;

    [Header("Knight Attack Var")]
    public int SingleMeleeVal;
    public Transform attackArea;
    public float giveDamage;
    public float attackingRadius;
    bool previouslyAttack;
    public float timebtwAttack;
    public Animator anim;

    private void Start()
    {
        curHealth = Maxhealth;
        _currentMovingSpeed = MovingSpeed;
        playerBody = GameObject.Find("Player");
    }

   

    private void Update()
    {
        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, playerLayer);
        playerInattackRadius = Physics.CheckSphere(transform.position, attackRadius, playerLayer);


        if (!playerInvisionRadius && !playerInattackRadius)
        {
            anim.SetBool("Idle", false);
            Walk();
        }

        if(playerInvisionRadius && !playerInattackRadius)
        {
            anim.SetBool("Idle", false);
            chasePlayer();
        }

        if (playerInvisionRadius && playerInattackRadius)
        {
            anim.SetBool("Idle", true);
            SingleMeleeModes();
        }

    }

    void chasePlayer()
    {
        _currentMovingSpeed = RunningSpeed;
        transform.position += _currentMovingSpeed * Time.deltaTime* transform.forward ;
        transform.LookAt(playerBody.transform);


        anim.SetBool("Walk", false);
        anim.SetBool("Attack", false);
        anim.SetBool("Run", true);


    }

    private void Walk()
    {
        _currentMovingSpeed = MovingSpeed;
        if (transform.position != destination)
        {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;
            float destinationDistance = destinationDirection.magnitude;

            if (destinationDistance >= StopSpeed)
            {
                destinationReached = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, TurningSpeed * Time.deltaTime);

                transform.Translate(_currentMovingSpeed * Time.deltaTime * Vector3.forward);

                anim.SetBool("Walk", true);
                anim.SetBool("Attack", false);
                anim.SetBool("Run", false);

            }
            else
            {
                destinationReached = true;
            }

        }
    }

    void SingleMeleeModes()
    {
        if (!previouslyAttack)
        {
            SingleMeleeVal = Random.Range(1, 5);

            if (SingleMeleeVal == 1)
            {
                Attack();
                StartCoroutine(Attack1());
            }

            if (SingleMeleeVal == 2)
            {
                Attack();
                StartCoroutine(Attack2());
            }

            if (SingleMeleeVal == 3)
            {
                Attack();
                StartCoroutine(Attack3());
            }

            if (SingleMeleeVal == 4)
            {
                Attack();
                StartCoroutine(Attack4());
            }
        }
    }


    void Attack()
    {
        Collider[] hitPlayer = Physics.OverlapSphere(attackArea.position, attackingRadius, playerLayer);

        foreach (Collider player in hitPlayer)
        {
            PlayerController playerScript = player.GetComponent<PlayerController>();

            if (playerScript != null)
            {
                Debug.Log("hitting player");
            }
        }

        previouslyAttack = true;
        Invoke(nameof(ActiveAttack), timebtwAttack);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackArea == null)
            return;

        Gizmos.DrawWireSphere(attackArea.position, attackingRadius);
    }

    public void LocateDestination(Vector3 destination)
    {
        this.destination = destination;
        destinationReached = false;
    }

    public void TakeDamage(float amount)
    {
        curHealth -= amount;
        anim.SetTrigger("GetHit");

        if (curHealth <= 0f)
        {
            Die();
        }
    }
    void Die()
    {
        anim.SetBool("IsDead", true);
        this.enabled = false;
        GetComponent<Collider>().enabled = false;
       // Destroy(gameObject);
    }

    private void ActiveAttack()
    {
        previouslyAttack = false;
    }

    IEnumerator Attack1()
    {
        anim.SetBool("Attack1", true);
        MovingSpeed = 0f;
        RunningSpeed = 0f;
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("Attack1", false);
        MovingSpeed = 1f;
        RunningSpeed = 3f;
    }

    IEnumerator Attack2()
    {
        anim.SetBool("Attack2", true);
        MovingSpeed = 0f;
        RunningSpeed = 0f;
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("Attack2", false);
        MovingSpeed = 1f;
        RunningSpeed = 3f;
    }

    IEnumerator Attack3()
    {
        anim.SetBool("Attack3", true);
        MovingSpeed = 0f;
        RunningSpeed = 0f;
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("Attack3", false);
        MovingSpeed = 1f;
        RunningSpeed = 3f;
    }

    IEnumerator Attack4()
    {
        anim.SetBool("Attack4", true);
        MovingSpeed = 0f;
        RunningSpeed = 0f;
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("Attack4", false);
        MovingSpeed = 1f;
        RunningSpeed = 3f;
    }


}
