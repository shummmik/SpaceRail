using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideListButtons : MonoBehaviour
{
    [SerializeField] private List<GameObject> enableObjects;
    [SerializeField] private List<GameObject> disableObjects;
    public void ActivateAddPiece(bool add)
    {
        foreach (GameObject respawn in enableObjects)
        {
            respawn.SetActive(add);
        }
        
        foreach (GameObject respawn in disableObjects)
        {
            respawn.SetActive(!add);
        }
        
    }
}
