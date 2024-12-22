using UnityEngine;

public class Star : PieceObject
{
    public override string GetName()
    {
        return "Star";
    }

    public override PieceMovement GetPieceMovement()
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Star;
    }
}
