using UnityEngine;

public class SettingState : BaseState
{

    public float minWait = 1;

    private float waitTime;
    public override void PrepareState()
    {
        base.PrepareState();

    }
    public override void UpdateState()
    {
        base.UpdateState();


        // owner.ChangeState(new MoveState());

    }
}