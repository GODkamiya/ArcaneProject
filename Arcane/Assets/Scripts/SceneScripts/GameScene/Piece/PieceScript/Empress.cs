using Fusion;
using UnityEngine;

public class Empress : PieceObject
{
    public override string GetName()
    {
        return "Empress";
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Empress;
    }
}
