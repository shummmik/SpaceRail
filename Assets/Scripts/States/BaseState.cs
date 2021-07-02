using System;
using UnityEngine;

[Serializable]
public abstract class BaseState
{

    public StateMachine owner;

    public virtual void PrepareState() { }

    public virtual void UpdateState() { }

    public virtual void DestroyState() { }
}