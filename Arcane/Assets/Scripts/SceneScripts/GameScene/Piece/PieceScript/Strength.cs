using UnityEngine;

public class Strength : PieceObject
{
    public override string GetName()
    {
        return "Strength";
    }

    public override PieceMovement GetPieceMovement()
    {
        PieceMovement pm = new PieceMovement();
        pm.range[x, y + 1] = true;
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Strength;
    }
}
