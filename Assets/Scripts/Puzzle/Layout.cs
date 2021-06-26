using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Layout : MonoBehaviour
{

    [SerializeField] private Camera _camera;
    public List<Piece> pieces;
    public bool Destroyed { get; private set; }
    public Piece CurrentInstancePrefab = null;
    private Piece CurrentInstance = null;
    private Piece currentCLosestPiece = null;
    
    private int width = Screen.width;
    private int height = Screen.height;
    public int currentClosestExit = -1;
    public int CurrentUsedExit = 0;

    private bool addPiece = false;
    private bool rotatePiece = false;
    
    void OnDestroy()
    {
        Destroyed = true;
    }

    private void Start()
    {   
        CurrentInstance = Instantiate(CurrentInstancePrefab, this.transform);
        CurrentInstance.name = "TempInstance";
    }

    void Update()
    {


        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        _camera.transform.position =  new Vector3(5 * horizontal * Time.deltaTime +_camera.transform.position.x,
            _camera.transform.position.y + 5 * vertical * Time.deltaTime,
            _camera.transform.position.z)  ;

        currentCLosestPiece = null;
        currentClosestExit = -1;

        
        if (rotatePiece)
        {
            rotatePiece = false;
            CurrentUsedExit = CurrentUsedExit + 1 >= CurrentInstance.Connectors.Length ? 0 : CurrentUsedExit + 1;
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
                    
                if(r == null)
                    continue;
                    
                for (int k = 0; k < r.Connectors.Length; ++k)
                {
                    if (r.ConnectorConnections[k] != null)
                        continue;
                    var guiPts = Camera.main.WorldToScreenPoint(r.Connectors[k].transform.position);
                    
                    float dist = (guiPts - new Vector3(width/2, height/2,0)).sqrMagnitude;

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
                CurrentInstance.transform.position = closest.position + CurrentInstance.transform.TransformVector(-usedExit.transform.localPosition);
            }
            
        }

        if (addPiece)
        {
            addPiece = false;
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
    }
    
    public void EnableRotate()
    {
        rotatePiece = true;
    }
    public void SelectPiece(Piece piece)
    {
        CurrentInstancePrefab = piece;
        Destroy(CurrentInstance.gameObject);
        
        CurrentInstance = Instantiate(CurrentInstancePrefab, this.transform);
        CurrentInstance.name = "TempInstance";
    }
}
