using Fusion;
using UnityEngine;

public class Empress : PieceObject
{
    public override string GetName()
    {
        return "Empress";
    }

    public override PieceMovement GetPieceMovement(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Empress;
    }
}
