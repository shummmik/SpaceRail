using System;
using System.Collections.Generic;
using UnityEngine;


public class StateMachine : Singleton<StateMachine>
{
    private BaseState currentState;

    private void Update()
    {

        if (currentState != null)
        {
            currentState.UpdateState();
        }
    }
    
    public void ChangeState(BaseState newState)
    {
        
        if (currentState != null)
        {
            currentState.DestroyState();
        }

        currentState = newState;
        if (currentState != null)
        {
            currentState.owner = this;
            currentState.PrepareState();
        }
    }


    
}