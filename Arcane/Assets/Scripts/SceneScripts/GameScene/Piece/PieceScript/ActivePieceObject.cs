using UnityEngine;

public abstract class ActivePieceObject : PieceObject
{
    public bool canActive = true;
    public abstract void ActiveEffect();
}
