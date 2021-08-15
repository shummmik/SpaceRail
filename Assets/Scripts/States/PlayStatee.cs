using UnityEngine;

public class PlayState : BaseState
{

    public override void PrepareState()
    {
        base.PrepareState();
        ObjectsManager.Instance.SetPlayState();
        // ObjectsManager.Instance.BallPlay();
        
        
        // for (int i = 0; i < owner.listObjectsEdit.Count; i++)
        // {
        //     owner.listObjectsEdit[i].SetActive(true);
        // }
        // for (int i = 0; i < owner.listObjectsPause.Count; i++)
        // {
        //     owner.listObjectsPause[i].SetActive(true);
        // } 
        // for (int i = 0; i < owner.listObjectsPlay.Count; i++)
        // {
        //     owner.listObjectsPlay[i].SetActive(false);
        // }
        // for (int i = 0; i < owner.listObjectsTransform.Count; i++)
        // {
        //     owner.listObjectsTransform[i].SetActive(false);
        // }
        
        SwitchState(owner.listObjectsEdit, true);
        SwitchState(owner.listObjectsPause, true);
        SwitchState(owner.listObjectsPlay, false);
        SwitchState(owner.listObjectsTransform, false);
        
        owner.panelEditor.SetActive(false);

    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (ObjectsManager.Instance.ball.transform.position.y < 0f)
            owner.ChangeState(new EditState());

        // owner.ChangeState(new MoveState());

    }
}
