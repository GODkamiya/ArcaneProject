using UnityEngine;

public class Tower : PieceObject
{
    public override string GetName()
    {
        return "Tower";
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Tower;
    }
}
