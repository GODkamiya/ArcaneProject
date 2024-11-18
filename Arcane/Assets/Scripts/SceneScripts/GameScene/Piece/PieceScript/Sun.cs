using UnityEngine;

public class Sun : PieceObject
{
    public override string GetName()
    {
        return "Sun";
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Sun;
    }
}
