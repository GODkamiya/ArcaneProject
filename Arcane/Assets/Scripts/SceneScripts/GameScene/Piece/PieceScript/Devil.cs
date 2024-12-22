using UnityEngine;

public class Devil : PieceObject
{
    public override string GetName()
    {
        return "Devil";
    }

    public override PieceMovement GetPieceMovement()
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Devil;
    }
}
