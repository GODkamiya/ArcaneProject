using UnityEngine;

public class Justice : PieceObject
{
    public override string GetName()
    {
        return "Justice";
    }

    public override PieceMovement GetPieceMovement()
    {
        PieceMovement pm = new PieceMovement();
        pm.range[x, y + 1] = true;
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Justice;
    }
}
