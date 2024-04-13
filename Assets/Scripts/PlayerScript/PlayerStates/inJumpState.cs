using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InJumpState : PlayerState
{
    // -----------  MovementInputs  -------------

    [Header("In Jump Variables")]

    [SerializeField] private Transform orientation;
    [SerializeField] private float maxJumpHeight;
    [SerializeField] private float maxJumpLenght;
    [SerializeField] private float timeToUncharge;
    private Vector3 velocity;


    public override void enterState(PlayerManager context)
    {
        Debug.Log("InJumpState");
        playerContext = context;
        velocity.y = 0f;

        Jump();
    }

    public override void updateState()
    {

        SwitchStateVerif();

    }

    public override void fixedUpdateState()
    {

        MovePlayer();

        playerContext.CameraTransform.localPosition = Vector3.Lerp(
            playerContext.CameraTransform.localPosition,
            playerContext.InitialCameraPosition,
            timeToUncharge * Time.deltaTime
        );

    }

    private void MovePlayer()
    {

        velocity.y += playerContext.Gravity * Time.deltaTime;

        playerContext.Controller.Move(velocity * Time.deltaTime);

    }

    private void Jump()
    {
        velocity = orientation.forward.normalized * playerContext.ActualPrctChargedJump * maxJumpLenght;
        velocity.y = Mathf.Sqrt(maxJumpHeight * playerContext.ActualPrctChargedJump * -playerContext.Gravity);

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
