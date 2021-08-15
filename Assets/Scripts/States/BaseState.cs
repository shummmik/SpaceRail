using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[Serializable]
public abstract class BaseState
{

    public StateMachine owner;

    protected void SwitchState(List<GameObject> list, bool state)
    {
        foreach (var elementlist in list)
        {
            elementlist.SetActive(state);
        }
        
    }
    public virtual void PrepareState() { }

    public virtual void UpdateState() { }

    public virtual void DestroyState() { }
}