using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FallingState : PlayerState
{
    // -----------  MovementInputs  -------------
    private float horizontalInput;
    private float verticalInput;

    [Header("Falling Variables")]

    [SerializeField] private float fallingWalkSpeed;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform orientation;
    private Vector3 moveDirection;
    private Vector3 velocity;


    [Header("Ground Check")]

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool grounded;

    public override void enterState(PlayerManager context)
    {
        Debug.Log("InFallingState");
        playerContext = context;
        velocity.y = 0f;
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
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        moveDirection.y = 0;

        playerContext.controller.Move(moveDirection.normalized * fallingWalkSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        playerContext.controller.Move(velocity * Time.deltaTime);

    }

    protected override void SwitchStateVerif()
    {

        if (grounded)
        {
            playerContext.nextState("inMovement");
        }
        
    }

    public override void exitState()
    {
        
    }

}
