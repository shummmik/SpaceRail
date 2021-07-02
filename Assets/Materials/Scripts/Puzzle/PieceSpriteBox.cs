using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PieceSprite
{
    public Piece piece;
    public Sprite sprite;
}

[CreateAssetMenu(menuName = "SpaseRail/PieceSprite Box", fileName = "PieceSpriteBox")]
public class PieceSpriteBox : ScriptableObject
{
    public PieceSprite[] pieceSprites;
}
