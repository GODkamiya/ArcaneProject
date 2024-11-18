using UnityEngine;

public class Devil : PieceObject
{
    public override string GetName()
    {
        return "Devil";
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Devil;
    }
}
