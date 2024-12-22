using UnityEngine;

public class HangedMan : PieceObject
{
    public override string GetName()
    {
        return "HangedMan";
    }

    public override PieceMovement GetPieceMovement()
    {
        PieceMovement pm = new PieceMovement();
        pm.range[x, y + 1] = true;
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.HangedMan;
    }
}
