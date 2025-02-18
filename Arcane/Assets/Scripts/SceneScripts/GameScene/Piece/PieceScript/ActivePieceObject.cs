using UnityEngine;

public abstract class ActivePieceObject : PieceObject
{
    public bool canActive = true;
    public abstract void ActiveEffect();

    /// <summary>
    /// そのコマが現在、技を使用可能な段階かを示す。
    /// </summary>
    public abstract bool CanSpellActiveEffect();
}
