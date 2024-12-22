using UnityEngine;

public class Sun : PieceObject
{
    public override string GetName()
    {
        return "Sun";
    }

    public override PieceMovement GetPieceMovement()
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Sun;
    }
}
