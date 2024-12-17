using UnityEngine;

public class Tower : PieceObject
{
    public override string GetName()
    {
        return "Tower";
    }

    public override PieceMovement GetPieceMovement()
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Tower;
    }
}
