using UnityEngine;

public class Moon : PieceObject
{
    public override string GetName()
    {
        return "Moon";
    }

    public override PieceMovement GetPieceMovement()
    {
        PieceMovement pm = new PieceMovement();
        pm.range[x, y + 1] = true;
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Moon;
    }
}
