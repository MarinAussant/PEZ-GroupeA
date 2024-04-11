using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [Header("Shared Components & Variables")]

    [SerializeField] private float timeToChargeJump;
    public float TimeToChargeJump
    {
        get { return timeToChargeJump; }
        set { timeToChargeJump = value; }
    }

    [SerializeField] private CharacterController controller;
    public CharacterController Controller
    {
        get { return controller; }
    }

    [SerializeField] private float gravity;
    public float Gravity
    {
        get { return gravity; }
    }

    [SerializeField] private Transform cameraTransform;
    public Transform CameraTransform
    {
        get { return cameraTransform; }
        set { cameraTransform = value; }
    }

    private Vector3 initialCameraPosition;
    public Vector3 InitialCameraPosition
    {
        get { return initialCameraPosition; }
        set { initialCameraPosition = value; }
    }

    private float actualPrctChargedJump;
    public float ActualPrctChargedJump
    {
        get { return actualPrctChargedJump; }
        set { actualPrctChargedJump = value; }
    }

    private Vector3 sharedVelocity;
    public Vector3 SharedVelocity
    {
        get { return sharedVelocity; }
        set { sharedVelocity = value; }
    }

    // --------------- Player States ------------------
    private PlayerState actualState;
    private Dictionary<string,PlayerState> listState;

    [SerializeField] private PlayerState inMovementState;
    [SerializeField] private PlayerState chargeJumpState;
    [SerializeField] private PlayerState fallingState;
    //[SerializeField] private PlayerState climbingState;
    [SerializeField] private PlayerState inJumpState;
    [SerializeField] private PlayerState inWaterState;
    [SerializeField] private PlayerState waterJumpState;





    private void Start()
    {
        initialCameraPosition = cameraTransform.localPosition;

        // ----------  Initialisation des states  ------------

        listState = new Dictionary<string,PlayerState>();

        listState.Add("inMovement",inMovementState);
        listState.Add("chargeJump", chargeJumpState);
        listState.Add("falling", fallingState);
        //listState.Add("climbing", climbingState);
        listState.Add("inJump", inJumpState);
        listState.Add("inWater", inWaterState);
        listState.Add("waterJump", waterJumpState);

        actualState = listState["inMovement"];
        actualState.enterState(this);
    }

    private void Update()
    {
        actualState.updateState();
    }

    private void FixedUpdate()
    {
        actualState.fixedUpdateState();    
    }

    public void nextState(string nextState)
    {
        actualState.exitState();
        actualState = listState[nextState];
        actualState.enterState(this);
    }




}
