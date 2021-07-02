using UnityEngine;

public class EditState: BaseState
{

    public override void PrepareState()
    {
        base.PrepareState();
        ObjectsManager.Instance.SetEditState();
    }
    public override void UpdateState()
    {
        base.UpdateState();


            // owner.ChangeState(new MoveState());

    }
}