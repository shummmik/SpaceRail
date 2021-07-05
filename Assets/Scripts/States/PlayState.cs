using UnityEngine;

public class PlayState : BaseState
{

    public override void PrepareState()
    {
        base.PrepareState();
        ObjectsManager.Instance.SetPlayState();
        ObjectsManager.Instance.BallPlay();
        
        
        for (int i = 0; i < owner.listObjectsEdit.Count; i++)
        {
            owner.listObjectsEdit[i].SetActive(true);
        }
        for (int i = 0; i < owner.listObjectsPause.Count; i++)
        {
            owner.listObjectsPause[i].SetActive(true);
        } 
        for (int i = 0; i < owner.listObjectsPlay.Count; i++)
        {
            owner.listObjectsPlay[i].SetActive(false);
        }
        

    }
    public override void UpdateState()
    {
        base.UpdateState();


        // owner.ChangeState(new MoveState());

    }
}
