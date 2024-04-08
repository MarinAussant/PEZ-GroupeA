using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private PlayerState actualState;

    private Dictionary<string,PlayerState> listState;

    [SerializeField] private PlayerState inMovementState;
    [SerializeField] private PlayerState loadingJumpState;
    [SerializeField] private PlayerState inJumpState;
    [SerializeField] private PlayerState inWaterState;

    //[SerializeField] private PlayerState loadingJumpState;



    private void Start()
    {

        // ----------  Initialisation des states  -------------

        listState = new Dictionary<string,PlayerState>();
        listState.Add("inMovement",inMovementState);
        listState.Add("loadingJumpState", loadingJumpState);
        listState.Add("inJumpState", inJumpState);
        listState.Add("inWaterState", inWaterState);
    }

    private void FixedUpdate()
    {
        actualState.updateState();    
    }

    public void nextState(string nextState)
    {
        actualState.exitState();
        actualState = listState[nextState];
        actualState.enterState();
    }
}
