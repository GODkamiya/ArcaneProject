using UnityEngine;

public class Death : PieceObject
{
    public override string GetName()
    {
        return "Death";
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Death;
    }
}
