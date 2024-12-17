using UnityEngine;

public class Hermit : PieceObject
{
    public override string GetName()
    {
        return "Hermit";
    }

    public override PieceMovement GetPieceMovement()
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Hermit;
    }
}
