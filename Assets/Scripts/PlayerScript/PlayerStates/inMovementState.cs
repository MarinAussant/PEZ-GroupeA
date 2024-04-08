using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InMovementState : PlayerState
{

    public override void enterState()
    {
        Debug.Log("Ca rentre !");
    }

    public override void updateState()
    {
        Debug.Log("C'est dedans !!");
    }

    public override void exitState()
    {
        
    }

}
