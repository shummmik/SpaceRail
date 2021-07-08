using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : Singleton<ObjectsManager>
{
    [SerializeField] private Camera camera;
    public GameObject ball;
    public Vector3 startPosition = Vector3.up*.5f;

    [SerializeField]  private Rigidbody rigidbodyBall;
    public Transform transformBall;
    
    [SerializeField] private List<Layout> layouts;
    [SerializeField] Piece selectPiece;
    public Layout SelectLayout { get; set; }
    [SerializeField] private Material outLineMaterial;

    private Vector3 velocityBall;
    
    
    private void Awake()
    {
        
        //rigidbodyBall = ball.GetComponent<Rigidbody>();
        InitializeLayout();
    }

    public void SetPiece(Piece piece)
    {
        selectPiece = piece;

        SelectLayout.SelectPiece(piece);
    }
    public void CreateLayout()
    {
        InitializeLayout();
    }
    
    
    public void DelLayout(Layout layout)
    {
        layouts.Remove(layout);
    }

    private void InitializeLayout()
    {
        GameObject instance = Instantiate(new GameObject());
        instance.AddComponent<Layout>();
        instance.name = "Layout"; 
        SelectLayout = instance.GetComponent<Layout>();
        SelectLayout.camera = camera;
        SelectLayout.CurrentInstancePrefab = selectPiece;
        SelectLayout.outLineMaterial = outLineMaterial;
        layouts.Add(SelectLayout);

        instance.transform.SetParent(transform, false);
        
    }

    public void AddPiece()
    {
        SelectLayout.AddPiece();
    }
    
    public void RotatePiece()
    {
        SelectLayout.EnableRotate();
    }
    
    public void DelPiece()
    {
        SelectLayout.DelPiece();
    }

    public void EnableDel()
    {
        SelectLayout.EnableDel();
    }
    
    public void EnableAdd()
    {
        SelectLayout.EnableAdd();
    }

    public void ResetPositionBall()
    {
        transformBall.position = startPosition;
    }
    
    public void SetPositionBall(Vector3 newPosition)
    {
        startPosition = newPosition;
        transformBall.position = startPosition;
    }
    
    public void SetPlayState()
    {
        SelectLayout.SetPlayState();
        BallPlay();
    }
    
    public void SetEditState()
    {
        SelectLayout.SetEditState();
        rigidbodyBall.useGravity = false;
        rigidbodyBall.velocity = Vector3.zero;
        transformBall.position = startPosition;
    }

    public void BallPlay()
    {
        rigidbodyBall.useGravity = true;
    }
    
    public void SetPauseState()
    {
        velocityBall = rigidbodyBall.velocity;
        rigidbodyBall.isKinematic = true;
        rigidbodyBall.useGravity = false;
    }

    public void PauseToPlayState()
    {
        rigidbodyBall.isKinematic = false;
        rigidbodyBall.useGravity = true;
        rigidbodyBall.velocity = velocityBall;
    }
    

}
