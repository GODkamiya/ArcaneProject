using Fusion;
using UnityEngine;

public class Empress : PieceObject
{
    public override string GetName()
    {
        return "Empress";
    }

    public override PieceMovement GetPieceMovement()
    {
        PieceMovement pm = new PieceMovement();
        pm.range[x, y + 1] = true;
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Empress;
    }
}
