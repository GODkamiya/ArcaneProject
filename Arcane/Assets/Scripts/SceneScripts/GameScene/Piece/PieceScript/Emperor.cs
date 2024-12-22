using UnityEngine;

public class Emperor : PieceObject
{
    public override string GetName()
    {
        return "Emperor";
    }

    public override PieceMovement GetPieceMovement()
    {
        PieceMovement pm = new PieceMovement();
        pm.range[x, y + 1] = true;
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Emperor;
    }
}
