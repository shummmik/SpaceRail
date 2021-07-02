using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPieceButton : MonoBehaviour
{
    [SerializeField] private List<GameObject> AddPiece;
    [SerializeField] private List<GameObject> DelPiece;
    public void ActivateAddPiece(bool add)
    {
        foreach (GameObject respawn in AddPiece)
        {
            respawn.SetActive(add);
        }
        
        foreach (GameObject respawn in DelPiece)
        {
            respawn.SetActive(!add);
        }
        
    }
}
