using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClimbingState : PlayerState
{
    // -----------  MovementInputs  -------------
    private float horizontalInput;
    private float verticalInput;

    [Header("Climbing Variables")]
    /*
    [SerializeField] private Transform orientation;
    [SerializeField] private float fallingWalkSpeed;
    [SerializeField] private float maxJumpHeight;
    */
    //private Vector3 moveDirection;
    private Vector3 velocity;

    [Header("Ground Check")]

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private bool grounded;

    public override void enterState(PlayerManager context)
    {
        Debug.Log("Climbing State");
        playerContext = context;
        velocity.y = 0f;

        //Jump();
    }

    public override void updateState()
    {
        if(velocity.y < 0f)
        {
            grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        }
        SwitchStateVerif();

    }

    public override void fixedUpdateState()
    {

        MovePlayer();

    }


    private void MovePlayer()
    {

        //moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        //moveDirection.y = 0;

        //playerContext.Controller.Move(moveDirection.normalized * fallingWalkSpeed * Time.deltaTime);

        float antigravity = 0.02f;
        float deltaY = (playerContext.TargetPoint.position.y - playerContext.CameraTransform.position.y) / 2;

        velocity.x = (playerContext.TargetPoint.position.x - playerContext.CameraTransform.position.x) * 2;
        velocity.z = (playerContext.TargetPoint.position.z - playerContext.CameraTransform.position.z) * 2;

        velocity.y += antigravity + deltaY;

        playerContext.Controller.Move(velocity * Time.deltaTime);

    }
    /*
    private void Jump()
    {
        velocity.y = Mathf.Sqrt(maxJumpHeight * -playerContext.Gravity);

        playerContext.Controller.Move(velocity * Time.deltaTime);
    }
    */

    protected override void SwitchStateVerif()
    {

        if (grounded)
        {

            playerContext.nextState("inMovement");

        }

    }

    public override void exitState()
    {
        playerContext.SharedVelocity = velocity;
        playerContext.TargetPoint = null;
        grounded = false;
    }

}
