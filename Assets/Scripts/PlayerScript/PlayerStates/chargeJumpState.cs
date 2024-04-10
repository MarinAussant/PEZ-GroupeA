using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChargeJumpState : PlayerState
{
    // -----------  MovementInputs  -------------
    private float timeJumpPressed = 0f;


    //private Vector3 initialCameraPosition;


    [Header("Charge Jump Variable")]

    [SerializeField] private float timeToChargeMax;
    [SerializeField] private Vector3 cameraOffset;

    public override void enterState(PlayerManager context)
    {
        Debug.Log("ChargeJumpState");
        playerContext = context;
        timeJumpPressed = 0f;
        //initialCameraPosition = cameraTransorm.localPosition;
    }

    public override void updateState()
    {

        MyInput();
        SwitchStateVerif();

    }

    public override void fixedUpdateState()
    {
        playerContext.CameraTransform.localPosition =  Vector3.Lerp(
            playerContext.CameraTransform.localPosition,
            playerContext.InitialCameraPosition + cameraOffset,
            (timeJumpPressed / timeToChargeMax) * 8 * Time.deltaTime
        );
    }

    private void MyInput()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            
            timeJumpPressed += Time.deltaTime;

        }

    }

    protected override void SwitchStateVerif()
    {

        if (Input.GetKeyUp(KeyCode.Space))
        {
            playerContext.nextState("inJump");
        }
        
    }

    public override void exitState()
    {
        if(timeJumpPressed > timeToChargeMax)
        {
            playerContext.ActualPrctChargedJump = 1;
        }
        else
        {
            playerContext.ActualPrctChargedJump = ((timeJumpPressed * 100) / timeToChargeMax) / 100;
        }
    }

}
