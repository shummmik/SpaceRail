using UnityEngine;

public class TransformState: BaseState
{



    private float waitTime;
    public override void PrepareState()
    {
        base.PrepareState();
        
        
        for (int i = 0; i < owner.listObjectsEdit.Count; i++)
        {
            owner.listObjectsEdit[i].SetActive(true);
        }
        for (int i = 0; i < owner.listObjectsPause.Count; i++)
        {
            owner.listObjectsPause[i].SetActive(false);
        } 
        for (int i = 0; i < owner.listObjectsPlay.Count; i++)
        {
            owner.listObjectsPlay[i].SetActive(true);
        }
        for (int i = 0; i < owner.listObjectsTransform.Count; i++)
        {
            owner.listObjectsTransform[i].SetActive(false);
        }
    }
    public override void UpdateState()
    {
        base.UpdateState();


        // owner.ChangeState(new MoveState());

    }
}
