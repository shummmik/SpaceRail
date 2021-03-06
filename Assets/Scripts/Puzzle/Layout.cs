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
    
    private int half_width = Screen.width/2;
    private int half_height = Screen.height/2;
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
                    CurrentInstance.transform.position = this.transform.TransformPoint(Vector3.zero);
                }
                else
                {

                    float closestSqrDist = float.MaxValue;
                    for (int i = 0; i < this.pieces.Count; ++i)
                    {
                        Piece r = pieces[i];

                        if (r == null)
                            continue;

                        for (int k = 0; k < r.Connectors.Length; ++k)
                        {
                            if (r.ConnectorConnections[k] != null)
                                continue;
                            // Transforms position from world space into screen space.
                            var guiPts = camera.WorldToScreenPoint(r.Connectors[k].transform.position);

                            float dist = (guiPts - new Vector3(half_width, half_height, 0)).sqrMagnitude;

                            if (dist < closestSqrDist)
                            {
                                closestSqrDist = dist;
                                currentCLosestPiece = r;
                                currentClosestExit = k;
                            }
                        }
                    }

                    if (currentCLosestPiece != null)
                    {
                        // CurrentInstance.transform.rotation = Quaternion.identity;
                        Transform closest = currentCLosestPiece.Connectors[currentClosestExit];
                        Transform usedExit = CurrentInstance.Connectors[CurrentUsedExit];
                        Quaternion targetRotation = Quaternion.LookRotation(-closest.forward, closest.up);
                        Quaternion difference = targetRotation * Quaternion.Inverse(usedExit.rotation);
                        // Quaternion rotation = CurrentInstance.transform.rotation * difference;
                        CurrentInstance.transform.rotation = Quaternion.identity * difference;
                        CurrentInstance.transform.position = closest.position +
                                                             CurrentInstance.transform.TransformVector(-usedExit
                                                                 .transform
                                                                 .localPosition);
                    }
                }
            }
            else
            {
                if (pieces.Count > 0)
                {

                    float closestSqrDist = float.MaxValue;
                    for (int i = 0; i < this.pieces.Count; ++i)
                    {
                        Piece r = pieces[i];

                        if (r == null)
                            continue;

                        for (int k = 0; k < r.Connectors.Length; ++k)
                        {
                            if (r.ConnectorConnections[k] != null)
                                continue;
                            var guiPts = Camera.main.WorldToScreenPoint(r.Connectors[k].transform.position);

                            float dist = (guiPts - new Vector3(half_width, half_height, 0)).sqrMagnitude;

                            if (dist < closestSqrDist)
                            {
                                closestSqrDist = dist;
                                currentCLosestPiece = r;
                                currentClosestExit = k;
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
            var newPiece = Instantiate(CurrentInstancePrefab);
            newPiece.transform.SetParent(this.transform, false);
            newPiece.transform.position = CurrentInstance.transform.position;
            newPiece.transform.rotation = CurrentInstance.transform.rotation;
            newPiece.transform.localScale = CurrentInstance.transform.localScale;

            newPiece.name = CurrentInstancePrefab.gameObject.name;
            newPiece.gameObject.isStatic = true;
            newPiece.Placed(this);
            pieces.Add(newPiece);
            if (currentCLosestPiece != null)
            {
                currentCLosestPiece.ConnectorConnections[currentClosestExit] = newPiece;
                newPiece.ConnectorConnections[CurrentUsedExit] = currentCLosestPiece;
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
        CurrentInstance = Instantiate(CurrentInstancePrefab, this.transform);
        CurrentInstance.name = "TempInstance";
        CurrentInstance.SetOutlineMaterial();
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
