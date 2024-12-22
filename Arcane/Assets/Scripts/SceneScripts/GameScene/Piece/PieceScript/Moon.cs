using UnityEngine;

public class Moon : PieceObject
{
    public override string GetName()
    {
        return "Moon";
    }

    public override PieceMovement GetPieceMovement()
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Moon;
    }
}
