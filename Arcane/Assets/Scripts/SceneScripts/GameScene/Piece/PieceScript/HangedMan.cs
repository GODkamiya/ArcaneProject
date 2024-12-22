using UnityEngine;

public class HangedMan : PieceObject
{
    public override string GetName()
    {
        return "HangedMan";
    }

    public override PieceMovement GetPieceMovement(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.HangedMan;
    }
}
