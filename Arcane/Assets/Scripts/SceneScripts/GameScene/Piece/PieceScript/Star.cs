using UnityEngine;

public class Star : PieceObject
{
    public override string GetName()
    {
        return "Star";
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Star;
    }
}
