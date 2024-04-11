using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FallingState : PlayerState
{
    // -----------  MovementInputs  -------------
    private float horizontalInput;
    private float verticalInput;
    private bool inWater;

    [Header("Falling Variables")]

    [SerializeField] private float fallingWalkSpeed;
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
        Debug.Log("InFallingState");
        playerContext = context;

        inWater = false;

        velocity = playerContext.SharedVelocity;
        velocity.y = 0f;

        playerContext.CameraTransform.localPosition = playerContext.InitialCameraPosition;
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

    }

    private void MovePlayer()
    {

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        moveDirection.y = 0;

        playerContext.Controller.Move(moveDirection.normalized * fallingWalkSpeed * Time.deltaTime);

        velocity.y += playerContext.Gravity * Time.deltaTime;

        playerContext.Controller.Move(velocity * Time.deltaTime);

    }

    private void Decelaration()
    {
        if (velocity.x < -decelerationSpeed)
        {
            velocity.x += decelerationSpeed;
        }
        else if (velocity.x > decelerationSpeed)
        {
            velocity.x -= decelerationSpeed;
        }
        else
        {
            velocity.x = 0;
        }

        if (velocity.z < -decelerationSpeed)
        {
            velocity.z += decelerationSpeed;
        }
        else if (velocity.z > decelerationSpeed)
        {
            velocity.z -= decelerationSpeed;
        }
        else
        {
            velocity.z = 0;
        }
    }

    protected override void SwitchStateVerif()
    {

        if (grounded)
        {

            playerContext.nextState("inMovement");

        }

        if (inWater)
        {

            playerContext.nextState("inWater");

        }

    }

    public override void exitState()
    {
        playerContext.SharedVelocity = velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            inWater = true;
        }
    }

}
