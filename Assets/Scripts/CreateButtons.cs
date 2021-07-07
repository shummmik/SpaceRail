using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CreateButtons : MonoBehaviour
{
    public GameObject button;
    // public ObjectsManager objectsManager;
    // public List<Piece> pieces;

    [SerializeField] private PieceSpriteBox pieceSpriteBox;

    void Start()
    {
        InstatiateButtons();
    }

    private void InstatiateButtons()
    {
        foreach (var pieceSprite in pieceSpriteBox.pieceSprites)
        {
            GameObject buttonPreview = Instantiate(button);
            buttonPreview.GetComponent<Image>().overrideSprite =  pieceSprite.sprite;
            buttonPreview.transform.SetParent(this.gameObject.transform);
            buttonPreview.GetComponent<Button>().onClick.AddListener(() => SetPiece(pieceSprite.piece));
            
        }
    }
    private void SetPiece(Piece piece)
    {
        ObjectsManager.Instance.SetPiece(piece);
    }

}
