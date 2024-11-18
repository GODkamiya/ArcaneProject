using UnityEngine;

public class Lovers : PieceObject
{
    public override string GetName()
    {
        return "Lovers";
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Lovers;
    }
}
