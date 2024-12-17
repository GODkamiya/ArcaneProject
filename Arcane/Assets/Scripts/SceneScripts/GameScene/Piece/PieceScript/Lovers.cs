using UnityEngine;

public class Lovers : PieceObject
{
    public override string GetName()
    {
        return "Lovers";
    }

    public override PieceMovement GetPieceMovement()
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Lovers;
    }
}
