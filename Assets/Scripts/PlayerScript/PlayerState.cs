using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{

    protected PlayerManager playerContext;

    virtual public void enterState(PlayerManager context)
    {

    }
    virtual public void updateState()
    {
        
    }
    virtual public void fixedUpdateState()
    {

    }
    virtual protected void SwitchStateVerif()
    {

    }
    virtual public void exitState()
    {

    }

}
