using UnityEngine;

public class PlayState : BaseState
{

    public override void PrepareState()
    {
        base.PrepareState();
        ObjectsManager.Instance.SetPlayState();

    }
    public override void UpdateState()
    {
        base.UpdateState();


        // owner.ChangeState(new MoveState());

    }
}
