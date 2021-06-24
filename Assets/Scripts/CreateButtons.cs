using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CreateButtons : MonoBehaviour
{
    public GameObject button;
    public Layout selectLayout;
    public List<Piece> pieces;

    void Start()
    {
        InstatiateButtons();
    }

    private void InstatiateButtons()
    {
        
        /*var asset = AssetDatabase.FindAssets("t:PieceBox");
        string path = AssetDatabase.GUIDToAssetPath(asset[0]);
        PieceBox palette = AssetDatabase.LoadAssetAtPath<PieceBox>(path);
        */
        foreach (var piece in pieces)
        {
             GameObject buttonPreview = Instantiate(button);
             /*
             var renderers = piece.gameObject.GetComponentInChildren<Renderer>();
             
             Sprite sprite = Sprite.Create( AssetPreview.GetAssetPreview(piece.gameObject), new Rect(0f,0f, 128f,128f),  Vector2.zero, 100.0f);
             if (renderers != null)
                 buttonPreview.GetComponent<Image>().overrideSprite =  sprite;
             */
             
            //Sets "ChoiceButtonHolder" as the new parent of the s1Button.
            buttonPreview.transform.SetParent(this.gameObject.transform);
            buttonPreview.GetComponent<Button>().onClick.AddListener(() => SetPiece(piece.gameObject.GetComponent<Piece>()));
            
        }
    }
    public  void SetPiece(Piece piece)
    {
        Debug.Log(piece);
        selectLayout.SelectPiece(piece);
    }

}
