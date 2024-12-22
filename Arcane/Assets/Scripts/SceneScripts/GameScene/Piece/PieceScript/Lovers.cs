using UnityEngine;

public class Lovers : PieceObject
{
    public override string GetName()
    {
        return "Lovers";
    }

    public override PieceMovement GetPieceMovement()
    {
        PieceMovement pm = new PieceMovement();
        pm.range[x, y + 1] = true;
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Lovers;
    }
}
