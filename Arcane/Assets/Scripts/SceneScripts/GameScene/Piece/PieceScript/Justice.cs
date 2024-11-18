using UnityEngine;

public class Justice : PieceObject
{
    public override string GetName()
    {
        return "Justice";
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Justice;
    }
}
