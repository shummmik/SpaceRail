using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Layout : MonoBehaviour
{
    public Camera camera;
    public List<Piece> pieces = new List<Piece>();
    public bool Destroyed { get; private set; }
    public Piece CurrentInstancePrefab = null;
    private Piece CurrentInstance = null;
    private Piece currentCLosestPiece = null;
    
    private int width = Screen.width;
    private int height = Screen.height;
    private int currentClosestExit = -1;
    private int CurrentUsedExit = 0;

    private bool edit = true;
    private bool addPiece = true;
    private bool rotatePiece = false;
 
     public Material outLineMaterial;
    
    void OnDestroy()
    {
        Destroyed = true;
    }

    void Update()
    {

        if (edit)
        {
            currentCLosestPiece = null;
            currentClosestExit = -1;

            if (addPiece)
            {
                if (rotatePiece)
                {
                    rotatePiece = false;
                    CurrentUsedExit = CurrentUsedExit + 1 >= CurrentInstance.Connectors.Length
                        ? 0
                        : CurrentUsedExit + 1;
                }

                if (pieces.Count == 0)
                {
                    CurrentInstance.transform.position = transform.position;
                }
                else
                {
                    float closestSqrDist = float.MaxValue;
                    
                    for (int i = 0; i < pieces.Count; ++i)
                    {
                        Piece pieceInList = pieces[i];
                        if (pieceInList == null)
                            continue;
                        
                        for (int numConnectorInPiece = 0; numConnectorInPiece < pieceInList.Connectors.Length; ++numConnectorInPiece)
                        {
                            if (pieceInList.ConnectorConnections[numConnectorInPiece] != null)
                                continue;
                            var guiPts = Camera.main.WorldToScreenPoint(pieceInList.Connectors[numConnectorInPiece].transform.position);

                            float dist = (guiPts - new Vector3(width / 2, height / 2, 0)).sqrMagnitude;

                            if (dist < closestSqrDist)
                            {
                                closestSqrDist = dist;
                                currentCLosestPiece = pieceInList;
                                currentClosestExit = numConnectorInPiece;
                            }
                        }
                    }

                    if (currentCLosestPiece != null)
                    {
                        CurrentInstance.transform.rotation = Quaternion.identity;
                        Transform closest = currentCLosestPiece.Connectors[currentClosestExit];
                        Transform usedExit = CurrentInstance.Connectors[CurrentUsedExit];
                        Quaternion targetRotation = Quaternion.LookRotation(-closest.forward, closest.up);
                        Quaternion difference = targetRotation * Quaternion.Inverse(usedExit.rotation);
                        Quaternion rotation = CurrentInstance.transform.rotation * difference;
                        CurrentInstance.transform.rotation = rotation;
                        CurrentInstance.transform.position = closest.position +
                                                             CurrentInstance.transform.TransformVector(
                                                                 -usedExit.transform.localPosition
                                                                 );
                    }
                }
            }
            else
            {
                if (pieces.Count > 0)
                {
                    float closestSqrDist = float.MaxValue;
                    foreach (var pieceInList in pieces)
                    {
                        if (pieceInList == null)
                            continue;

                        for (int numConnectorInPiece = 0; numConnectorInPiece < pieceInList.Connectors.Length; ++numConnectorInPiece)
                        {
                            if (pieceInList.ConnectorConnections[numConnectorInPiece] != null)
                                continue;
                            var guiPts = camera.WorldToScreenPoint(pieceInList.Connectors[numConnectorInPiece].transform.position);
                            float dist = (guiPts - new Vector3(width / 2, height / 2, 0)).sqrMagnitude;
                            if (dist < closestSqrDist)
                            {
                                closestSqrDist = dist;
                                currentCLosestPiece = pieceInList;
                                currentClosestExit = numConnectorInPiece;
                            }
                        }
                    }

                    if (currentCLosestPiece != null)
                    {
                        MeshFilter filter = currentCLosestPiece.meshFilter;

                        if (filter != null)
                        {
                            Matrix4x4 matrix = Matrix4x4.TRS(currentCLosestPiece.transform.position,
                                currentCLosestPiece.transform.rotation, currentCLosestPiece.transform.localScale);
                            Graphics.DrawMesh(filter.sharedMesh, matrix, outLineMaterial, 0);
                        }
                    }
                }


            }
        }
    }

    public void DelPiece()
    {
        if (!addPiece && currentCLosestPiece != null)
        {
            currentCLosestPiece.Removed();
            pieces.Remove(currentCLosestPiece);
            Destroy(currentCLosestPiece.gameObject);
        }
    }

    public void AddPiece()
    {
        if (addPiece)
        {
            var current = Instantiate(CurrentInstancePrefab);
            current.transform.SetParent(this.transform, false);
            current.transform.position = CurrentInstance.transform.position;
            current.transform.rotation = CurrentInstance.transform.rotation;
            current.transform.localScale = CurrentInstance.transform.localScale;

            current.name = CurrentInstancePrefab.gameObject.name;
            current.gameObject.isStatic = true;
            current.Placed(this);
            pieces.Add(current);
            if (currentCLosestPiece != null)
            {
                currentCLosestPiece.ConnectorConnections[currentClosestExit] = current;
                current.ConnectorConnections[CurrentUsedExit] = currentCLosestPiece;
            }
        }
    }
    
    public void EnableAdd()
    {

        addPiece = true;
        if (CurrentInstance == null)
            CreateCurrentInstance();
    }

    private void CreateCurrentInstance()
    {
        CurrentInstance = Instantiate(CurrentInstancePrefab,transform);
        CurrentInstance.name = "TempInstance";
        CurrentInstance.gameObject.GetComponent<MeshRenderer>().material = outLineMaterial;
        
    }
    
    public void EnableDel()
    {
        addPiece = false;
        if (CurrentInstance != null)
            Destroy(CurrentInstance.gameObject);
    }
    
    public void EnableRotate()
    {
        rotatePiece = true;
    }
    public void SelectPiece(Piece piece)
    {
        CurrentInstancePrefab = piece;
        Destroy(CurrentInstance.gameObject);
        
        CreateCurrentInstance();
    }

    public void SetPlayState()
    {
        edit = false;
        if (CurrentInstance != null)
            Destroy(CurrentInstance.gameObject);
    }
    
    public void SetEditState()
    {
        edit = true;
        if (CurrentInstance == null)
            CreateCurrentInstance();
    }
}
