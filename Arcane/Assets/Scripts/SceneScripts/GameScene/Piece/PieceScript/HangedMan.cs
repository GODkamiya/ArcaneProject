using UnityEngine;

public class HangedMan : PieceObject
{
    public override string GetName()
    {
        return "HangedMan";
    }

    public override PieceType GetPieceType()
    {
        return PieceType.HangedMan;
    }
}
