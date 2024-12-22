using UnityEngine;

public class Devil : PieceObject
{
    public override string GetName()
    {
        return "Devil";
    }

    public override PieceMovement GetPieceMovement()
    {
        PieceMovement pm = new PieceMovement();
        pm.range[x, y + 1] = true;
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Devil;
    }
}
