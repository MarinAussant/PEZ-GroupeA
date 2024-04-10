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


    [Header("Movement Variables")]

    [SerializeField] private float speed;
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

        playerContext.CameraTransform.localPosition = playerContext.InitialCameraPosition;

        velocity = playerContext.SharedVelocity;
    }

    public override void updateState()
    {

        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        MyInput();
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

    protected override void SwitchStateVerif()
    {

        if (Input.GetKeyUp(KeyCode.Space))
        {
            playerContext.nextState("climbing");
        }

        if (timeJumpPressed > playerContext.TimeToChargeJump)
        {
            playerContext.nextState("chargeJump");
        }

        if (!grounded)
        {
            playerContext.nextState("falling");
        }
        
    }

    public override void exitState()
    {
        
    }

}
