using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    public float movementSpeed = 3f;
    public CameraController cameraController;
    [SerializeField]
    private float _rotSpeed = 500f;
    Quaternion _requiredRotation;

    [Header("Player Animator")]
    public Animator PlayerAnimator;

    [Header("Player character controller and gravity")]
    public CharacterController CC;
    public float CheckSurfaceRadius = 0.2f;
    public Vector3 SurfaceCheackOffset;
    public LayerMask SurfaceLayer;
    bool onSurface;
    [SerializeField]
    float FallingSpeed;
    [SerializeField]
    Vector3 moveDir;

    private void Start()
    {
       
    }
    private void Update()
    {
        applyGravity();
        PlayerMovement();
        surfaceCheck();
        Debug.Log(onSurface);
    }
    void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

   

        float movementAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        var movementInput = (new Vector3(horizontal, 0, vertical)).normalized;

        var movementDir = cameraController.FlatRotation * movementInput;

        CC.Move(movementDir * movementSpeed * Time.deltaTime);
        if (movementAmount > 0.3)
        {
             //transform.position += movementDir * movementSpeed * Time.deltaTime;
          
            _requiredRotation = Quaternion.LookRotation(movementDir);
        }

        moveDir = movementDir; 
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _requiredRotation,_rotSpeed*Time.deltaTime);
        PlayerAnimator.SetFloat("movementValue", movementAmount,0.2f,Time.deltaTime);
    }

    void surfaceCheck()
    {
        onSurface = Physics.CheckSphere(transform.TransformPoint(SurfaceCheackOffset), CheckSurfaceRadius, SurfaceLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.TransformPoint(SurfaceCheackOffset), CheckSurfaceRadius);
    }

    void applyGravity()
    {
        if(onSurface)
        {
            FallingSpeed = -0.5f;

        }
        else
        {
            FallingSpeed += Physics.gravity.y * Time.deltaTime;
        }

        var velocity = moveDir * movementSpeed;
        velocity.y = FallingSpeed;
    }

}
