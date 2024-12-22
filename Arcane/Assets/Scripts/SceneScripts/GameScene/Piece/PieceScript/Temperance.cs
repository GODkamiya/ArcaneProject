using UnityEngine;

public class Temperance : PieceObject
{
    public override string GetName()
    {
        return "Temperance";
    }

    public override PieceMovement GetPieceMovement()
    {
        PieceMovement pm = new PieceMovement();
        pm.range[x, y + 1] = true;
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Temperance;
    }
}
