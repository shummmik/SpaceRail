using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : Singleton<ObjectsManager>
{
    [SerializeField] private List<Layout> layouts;
    [SerializeField] Piece selectPiece;
    public Layout SelectLayout { get; set; }
    [SerializeField] private Material outLineMaterial;
    
    private void Awake()
    {
        InitializeLayout();
    }

    public void SetPiece(Piece piece)
    {
        selectPiece = piece;
        Debug.Log(SelectLayout);
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
}
