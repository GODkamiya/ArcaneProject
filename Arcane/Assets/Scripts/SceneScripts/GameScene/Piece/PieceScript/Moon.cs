using UnityEngine;

public class Moon : PieceObject
{
    public override string GetName()
    {
        return "Moon";
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Moon;
    }
}
