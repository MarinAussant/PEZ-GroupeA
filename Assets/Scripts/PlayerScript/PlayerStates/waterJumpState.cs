using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WaterJumpState : PlayerState
{
    // -----------  MovementInputs  -------------
    private float horizontalInput;
    private float verticalInput;

    [Header("Water Jump Variables")]

    [SerializeField] private Transform orientation;
    [SerializeField] private float fallingWalkSpeed;
    [SerializeField] private float maxJumpHeight;
    private Vector3 moveDirection;
    private Vector3 velocity;


    public override void enterState(PlayerManager context)
    {
        Debug.Log("WaterJumpState");
        playerContext = context;
        velocity.y = 0f;

        Jump();
    }

    public override void updateState()
    {

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

        playerContext.Controller.Move(moveDirection.normalized * fallingWalkSpeed * Time.deltaTime);

        velocity.y += playerContext.Gravity * Time.deltaTime;

        playerContext.Controller.Move(velocity * Time.deltaTime);

    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(maxJumpHeight * -playerContext.Gravity);

        playerContext.Controller.Move(velocity * Time.deltaTime);
    }

    protected override void SwitchStateVerif()
    {

        if(velocity.y < 0)
        {
            playerContext.nextState("falling");
        }
            
    }

    public override void exitState()
    {
        playerContext.SharedVelocity = velocity;
    }

}
