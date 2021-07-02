using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{

    public delegate void ClickAction(Piece piece);
    public event ClickAction SetPiecePrefab;
    
}
