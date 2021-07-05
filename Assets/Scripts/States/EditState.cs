using UnityEngine;

public class EditState: BaseState
{

    public override void PrepareState()
    {
        base.PrepareState();
        ObjectsManager.Instance.SetEditState();
        
        for (int i = 0; i < owner.listObjectsEdit.Count; i++)
        {
            owner.listObjectsEdit[i].SetActive(false);
        }
        for (int i = 0; i < owner.listObjectsPause.Count; i++)
        {
            owner.listObjectsPause[i].SetActive(false);
        } 
        for (int i = 0; i < owner.listObjectsPlay.Count; i++)
        {
            owner.listObjectsPlay[i].SetActive(true);
        }
    }
    public override void UpdateState()
    {
        base.UpdateState();


            // owner.ChangeState(new MoveState());

    }
}