using UnityEngine;

public class Strength : PieceObject
{
    public override string GetName()
    {
        return "Strength";
    }

    public override PieceMovement GetPieceMovement()
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Strength;
    }
}
