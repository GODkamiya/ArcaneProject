using UnityEngine;

public class Magician : PieceObject
{
    public override string GetName()
    {
        return "Magician";
    }

    public override PieceMovement GetPieceMovement()
    {
        PieceMovement pm = new PieceMovement();
        pm.range[x, y + 1] = true;
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Magician;
    }
}
