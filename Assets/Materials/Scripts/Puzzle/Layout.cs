using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Layout : MonoBehaviour
{

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

    private void Start()
    {
        // CurrentInstancePrefab = 
        if (addPiece)
        {
            CreateCurrentInstance();
        }
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
                            var guiPts = Camera.main.WorldToScreenPoint(r.Connectors[k].transform.position);

                            float dist = (guiPts - new Vector3(width / 2, height / 2, 0)).sqrMagnitude;

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
                        CurrentInstance.transform.rotation = Quaternion.identity;
                        Transform closest = currentCLosestPiece.Connectors[currentClosestExit];
                        Transform usedExit = CurrentInstance.Connectors[CurrentUsedExit];
                        Quaternion targetRotation = Quaternion.LookRotation(-closest.forward, closest.up);
                        Quaternion difference = targetRotation * Quaternion.Inverse(usedExit.rotation);
                        Quaternion rotation = CurrentInstance.transform.rotation * difference;
                        CurrentInstance.transform.rotation = rotation;
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

                            float dist = (guiPts - new Vector3(width / 2, height / 2, 0)).sqrMagnitude;

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
                        // currentCLosestPiece.gameObject.GetComponent<MeshRenderer>().material = outLineMaterial;
                        MeshFilter filter = currentCLosestPiece.gameObject.GetComponentInChildren<MeshFilter>();

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
        if (!addPiece)
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
            var c = Instantiate(CurrentInstancePrefab);
            c.transform.SetParent(this.transform, false);
            c.transform.position = CurrentInstance.transform.position;
            c.transform.rotation = CurrentInstance.transform.rotation;
            c.transform.localScale = CurrentInstance.transform.localScale;

            c.name = CurrentInstancePrefab.gameObject.name;
            c.gameObject.isStatic = true;
            c.Placed(this);
            pieces.Add(c);
            if (currentCLosestPiece != null)
            {
                currentCLosestPiece.ConnectorConnections[currentClosestExit] = c;
                c.ConnectorConnections[CurrentUsedExit] = currentCLosestPiece;
            }
        }
    }
    
    public void EnableAdd()
    {

        addPiece = true;
        CreateCurrentInstance();
    }

    private void CreateCurrentInstance()
    {
        CurrentInstance = Instantiate(CurrentInstancePrefab, this.transform);
        CurrentInstance.name = "TempInstance";
        CurrentInstance.gameObject.GetComponent<MeshRenderer>().material = outLineMaterial;
        // mRender.material = outLineMaterial;
            // SetValue(outLine, 1);

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
    }
    
    public void SetEditState()
    {
        edit = true;
    }
}
