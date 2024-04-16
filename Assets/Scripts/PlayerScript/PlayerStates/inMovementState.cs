using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InMovementState : PlayerState
{
    // -----------  MovementInputs  -------------
    private float horizontalInput;
    private float verticalInput;
    private float timeJumpPressed = 0f;
    private bool inWater;


    [Header("Movement Variables")]

    [SerializeField] private float speed;
    [SerializeField] private float decelerationSpeed;
    [SerializeField] private Transform orientation;
    private Vector3 moveDirection;
    private Vector3 velocity;


    [Header("Ground Check")]

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private bool grounded;

    public override void enterState(PlayerManager context)
    {
        Debug.Log("InMovementState");
        playerContext = context;
        timeJumpPressed = 0f;

        inWater = false;

        playerContext.CameraTransform.localPosition = playerContext.InitialCameraPosition;

        velocity = playerContext.SharedVelocity;
        playerContext.SharedVelocity = Vector3.zero;
    }

    public override void updateState()
    {

        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        MyInput();
        Decelaration();
        SwitchStateVerif();

    }

    public override void fixedUpdateState()
    {

        MovePlayer();

    }

    private void MyInput()
    {

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space))
        {
            timeJumpPressed += Time.deltaTime;
        }

    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        moveDirection.y = 0;

        playerContext.Controller.Move(moveDirection.normalized * speed * Time.deltaTime);

        velocity.y = -1f;

        playerContext.Controller.Move(velocity * Time.deltaTime);

    }

    private void Decelaration()
    {
        if (velocity.x < -decelerationSpeed)
        {
            velocity.x += decelerationSpeed * Time.deltaTime;
        }
        else if(velocity.x > decelerationSpeed)
        {
            velocity.x -= decelerationSpeed * Time.deltaTime;
        }
        else
        {
            velocity.x = 0;
        }

        if (velocity.z < -decelerationSpeed)
        {
            velocity.z += decelerationSpeed * Time.deltaTime;
        }
        else if (velocity.z > decelerationSpeed)
        {
            velocity.z -= decelerationSpeed * Time.deltaTime;
        }
        else
        {
            velocity.z = 0;
        }
    }

    protected override void SwitchStateVerif()
    {

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (Physics.Raycast(playerContext.CameraTransform.position, Camera.main.transform.forward, out var hit, 0.10f))
            {
                if (hit.collider.gameObject.tag == "ClimbingTarget")
                {
                    
                    playerContext.TargetPoint = hit.collider.gameObject.transform;
                    playerContext.nextState("climbing");
                }
            }

            timeJumpPressed = 0f;

        }

        if (timeJumpPressed > playerContext.TimeToChargeJump)
        {

            playerContext.nextState("chargeJump");

        }

        if (!grounded)
        {

            playerContext.nextState("falling");

        }

        if (inWater)
        {

            playerContext.nextState("inWater");

        }

    }

    public override void exitState()
    {
        timeJumpPressed = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            inWater = true;
        }
    }

}
