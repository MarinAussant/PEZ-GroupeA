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

        playerContext.cameraTransform.localPosition = Vector3.Lerp(
            playerContext.cameraTransform.localPosition,
            playerContext.initialCameraPosition,
            timeToUncharge * Time.deltaTime
        );

    }

    private void MovePlayer()
    {

        velocity.y += playerContext.gravity * Time.deltaTime;

        playerContext.controller.Move(velocity * Time.deltaTime);

    }

    private void Jump()
    {
        velocity = orientation.forward.normalized * playerContext.actualPrctChargedJump * maxJumpLenght;
        velocity.y = Mathf.Sqrt(maxJumpHeight * playerContext.actualPrctChargedJump * -playerContext.gravity);

        playerContext.controller.Move(velocity * Time.deltaTime);
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
        
    }

}
