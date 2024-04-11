using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InWaterState : PlayerState
{
    // -----------  MovementInputs  -------------
    private float horizontalInput;
    private float verticalInput;
    private bool jumpInput;
    private bool inWater;


    [Header("Water Variables")]
    [SerializeField] private float waterGravity;
    [SerializeField] private float waterSpeed;
    [SerializeField] private float decelerationSpeed;
    [SerializeField] private float waterDiveUpSpeed;
    [SerializeField] private Transform orientation;
    private Vector3 moveDirection;
    private Vector3 velocity;


    public override void enterState(PlayerManager context)
    {
        Debug.Log("InWater");
        playerContext = context;
        inWater = true;

        playerContext.CameraTransform.localPosition = playerContext.InitialCameraPosition;

        velocity = playerContext.SharedVelocity;
        //playerContext.SharedVelocity = Vector3.zero;

    }

    public override void updateState()
    {

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
            jumpInput = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpInput = false;
        }

    }

    private void MovePlayer()
    {
        if (jumpInput)
        {
            JumpWater();
        }

        if (velocity.y > -waterGravity)
        {
            velocity.y -= 0.1f;
        }
        else if (velocity.y < -waterGravity)
        {
            velocity.y += 0.1f;
        }
        else 
        {
            velocity.y = -waterGravity;
        }

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        playerContext.Controller.Move(moveDirection.normalized * waterSpeed * Time.deltaTime);
        playerContext.Controller.Move(velocity * Time.deltaTime);

    }

    private void JumpWater()
    {
        if (velocity.y < waterDiveUpSpeed)
        {
            velocity.y += Mathf.Sqrt(waterDiveUpSpeed);
        }
        
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

        if (!inWater)
        {

            playerContext.ActualPrctChargedJump = 0.5f;
            playerContext.nextState("waterJump");

        }
        
    }

    public override void exitState()
    {
        jumpInput = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            inWater = false;
        }
    }

}
