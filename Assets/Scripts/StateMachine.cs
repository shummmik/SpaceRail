using System;
using System.Collections.Generic;
using UnityEngine;


public class StateMachine : Singleton<StateMachine>
{
    private BaseState currentState;

    public GameObject panelEditor;
    public List<GameObject> listObjectsPlay;
    public List<GameObject> listObjectsPause;
    public List<GameObject> listObjectsEdit;

    private void Start()
    {
        ChangeState(new EditState());
    }

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