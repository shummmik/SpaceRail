using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : Singleton<ObjectsManager>
{
    [SerializeField] private GameObject ball;
    [SerializeField] private Vector3 startPosition = Vector3.up*.5f;

    private Rigidbody rigidbodyBall;
    
    [SerializeField] private List<Layout> layouts;
    [SerializeField] Piece selectPiece;
    public Layout SelectLayout { get; set; }
    [SerializeField] private Material outLineMaterial;

    private CollisionDetectionMode ballDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    // private float massBall = 2.5f;
    // private float dradBall = 0.2f;
    // private float angularDrag = 0.2f;
    private Vector3 velocityBall;
    
    
    private void Awake()
    {
        
        rigidbodyBall = ball.GetComponent<Rigidbody>();
        InitializeLayout();
    }

    public void SetPiece(Piece piece)
    {
        selectPiece = piece;
        //Debug.Log(SelectLayout);
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
        ball.transform.position = startPosition;
    }
    
    public void SetPositionBall(Vector3 newPosition)
    {
        startPosition = newPosition;
        ball.transform.position = startPosition;
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

        ball.transform.position = startPosition;

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
        // Destroy(ball.GetComponent<Rigidbody>());

    }

    public void PauseToPlayState()
    {
        rigidbodyBall.isKinematic = false;
        rigidbodyBall.useGravity = true;
        rigidbodyBall.velocity = velocityBall;
    }
}
