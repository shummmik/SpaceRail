using UnityEngine;

public class EditState: BaseState
{

    public override void PrepareState()
    {
        base.PrepareState();
        ObjectsManager.Instance.SetEditState();
        
        // for (int i = 0; i < owner.listObjectsEdit.Count; i++)
        // {
        //     owner.listObjectsEdit[i].SetActive(false);
        // }
        // for (int i = 0; i < owner.listObjectsPause.Count; i++)
        // {
        //     owner.listObjectsPause[i].SetActive(false);
        // } 
        // for (int i = 0; i < owner.listObjectsPlay.Count; i++)
        // {
        //     owner.listObjectsPlay[i].SetActive(true);
        // }
        // for (int i = 0; i < owner.listObjectsTransform.Count; i++)
        // {
        //     owner.listObjectsTransform[i].SetActive(true);
        // }
        
        SwitchState(owner.listObjectsEdit, false);
        SwitchState(owner.listObjectsPause, false);
        SwitchState(owner.listObjectsPlay, true);
        SwitchState(owner.listObjectsTransform, true);
        
        owner.panelEditor.SetActive(true);
    }
    public override void UpdateState()
    {
        base.UpdateState();


            // owner.ChangeState(new MoveState());

    }
}