using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [Header("Shared Component")]
    public float timeToChargeJump;
    public CharacterController controller;


    private PlayerState actualState;
    private Dictionary<string,PlayerState> listState;

    [SerializeField] private InMovementState inMovementState;
    //[SerializeField] private PlayerState chargeJumpState;
    [SerializeField] private PlayerState fallingState;
    //[SerializeField] private PlayerState climbingState;
    //[SerializeField] private PlayerState inJumpState;
    //[SerializeField] private PlayerState inWaterState;





    private void Start()
    {

        // ----------  Initialisation des states  ------------

        listState = new Dictionary<string,PlayerState>();
        listState.Add("inMovement",inMovementState);
        //listState.Add("chargeJump", chargeJumpState);
        listState.Add("falling", fallingState);
        //listState.Add("climbing", climbingState);



        //listState.Add("inJump", inJumpState);
        //listState.Add("inWater", inWaterState);
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
