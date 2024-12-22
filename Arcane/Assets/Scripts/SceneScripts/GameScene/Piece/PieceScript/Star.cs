using UnityEngine;

public class Star : PieceObject
{
    public override string GetName()
    {
        return "Star";
    }

    public override PieceMovement GetPieceMovement()
    {
        PieceMovement pm = new PieceMovement();
        pm.range[x, y + 1] = true;
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Star;
    }
}
