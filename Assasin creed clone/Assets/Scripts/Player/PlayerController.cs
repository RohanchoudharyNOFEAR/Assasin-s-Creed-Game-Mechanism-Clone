using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Health & Energy")]
    private float playerHealth = 200f;
    public float presentHealth;
    private float playerEnergy = 100f;
    public float presentEnergy;
    public HealthBar healthbar;
    public EnergyBar energybar;
    public GameObject DamageIndicator;

    [Header("Player Movement")]
    public float movementSpeed = 3f;
    public CameraController cameraController;
    [SerializeField]
    public float RotSpeed = 500f;
    Quaternion _requiredRotation;
    bool playerControl = true;
    public bool HasPlayerControl { get { return playerControl; } set { playerControl = value; } }
    public EnvironmentChecker EnvironmentChecker;

    [Header("Player Animator")]
    public Animator PlayerAnimator;

    [Header("Player character controller and gravity")]
    public CharacterController CC;
    public float CheckSurfaceRadius = 0.2f;
    public Vector3 SurfaceCheackOffset;
    public LayerMask SurfaceLayer;
    public bool PlayerOnLedge;
    public LedgeInfo LedgeInfo { get; set; }
    bool onSurface;
    [SerializeField]
    float FallingSpeed;
    [SerializeField]
    Vector3 moveDir;
    Vector3 requiredMovementDir;
    Vector3 velocity;

    private void Start()
    {
        presentHealth = playerHealth;
        presentEnergy = playerEnergy;
        healthbar.GiveFullHealth(presentHealth);
        energybar.GiveFullEnergy(presentEnergy);
    }
    private void Update()
    {
        if (presentEnergy <= 0)
        {
            movementSpeed = 2f;

            if (!Input.GetButton("Horizontal") || !Input.GetButton("Vertical"))
            {
                PlayerAnimator.SetFloat("movementValue", 0f);
            }

            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                PlayerAnimator.SetFloat("movementValue", 0.5f);
                StartCoroutine(setEnergy());
            }
        }

        if (presentEnergy >= 1)
        {
            movementSpeed = 5f;
        }

        if (PlayerAnimator.GetFloat("movementValue") >= 0.9999)
        {
            playerEnergyDecrease(0.02f);
        }



        surfaceCheck();
        if (!playerControl)
            return;
        applyGravity();
        PlayerMovement();

        Debug.Log(onSurface);
    }
    void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");



        float movementAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        var movementInput = (new Vector3(horizontal, 0, vertical)).normalized;

        //  var movementDir = cameraController.FlatRotation * movementInput;
        requiredMovementDir = cameraController.FlatRotation * movementInput;

        // CC.Move(movementDir * movementSpeed * Time.deltaTime);
        CC.Move(velocity * Time.deltaTime);
        if (movementAmount > 0.1 && moveDir.magnitude >0.2f)
        {
            //transform.position += movementDir * movementSpeed * Time.deltaTime;

            _requiredRotation = Quaternion.LookRotation(moveDir);
        }

        moveDir = requiredMovementDir;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _requiredRotation, RotSpeed * Time.deltaTime);
       // PlayerAnimator.SetFloat("movementValue", movementAmount, 0.2f, Time.deltaTime);
    }

    void surfaceCheck()
    {
        onSurface = Physics.CheckSphere(transform.TransformPoint(SurfaceCheackOffset), CheckSurfaceRadius, SurfaceLayer);
        PlayerAnimator.SetBool("onSurface", onSurface);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.TransformPoint(SurfaceCheackOffset), CheckSurfaceRadius);
    }

    void applyGravity()
    {
        velocity = Vector3.zero;
        if (onSurface)
        {
            FallingSpeed = -0.5f;
            velocity = moveDir * movementSpeed;
            PlayerOnLedge = EnvironmentChecker.CheckLedge(moveDir, out LedgeInfo ledgeInfo);
            if (PlayerOnLedge)
            {
                LedgeInfo = ledgeInfo;
                playerLedgeMovement();
                Debug.Log("player on ledge");
            }
            PlayerAnimator.SetFloat("movementValue", velocity.magnitude/movementSpeed, 0.2f, Time.deltaTime);
        }
        else
        {
            FallingSpeed += Physics.gravity.y * Time.deltaTime;
            velocity = transform.forward * movementSpeed / 2;
        }


        velocity.y = FallingSpeed;
    }

    public void SetControl(bool hasControl)
    {
        this.playerControl = hasControl;
        CC.enabled = hasControl;

        if (!hasControl)
        {
            PlayerAnimator.SetFloat("movementValue", 0f);
            _requiredRotation = transform.rotation;
        }
    }

    void playerLedgeMovement()
    {
        float angle = Vector3.Angle(LedgeInfo.SurfaceHit.normal, requiredMovementDir);
        if(angle<90)
        {
            velocity = Vector3.zero;
            moveDir = Vector3.zero;
        }
    }


    public void playerHitDamage(float takeDamage)
    {
      
        presentHealth -= takeDamage;
        healthbar.SetHealth(presentHealth);
        StartCoroutine(showDamage());

        if (presentHealth <= 0)
        {
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        Cursor.lockState = CursorLockMode.None;
        Object.Destroy(gameObject, 1.0f);
    }

    public void playerEnergyDecrease(float energyDecrease)
    {
        presentEnergy -= energyDecrease;
        energybar.SetEnergy(presentEnergy);
    }

    IEnumerator setEnergy()
    {
        presentEnergy = 0f;
        yield return new WaitForSeconds(5f);
        energybar.GiveFullEnergy(presentEnergy);
        presentEnergy = 100f;
    }

    IEnumerator showDamage()
    {
        DamageIndicator.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        DamageIndicator.SetActive(false);
    }


}
