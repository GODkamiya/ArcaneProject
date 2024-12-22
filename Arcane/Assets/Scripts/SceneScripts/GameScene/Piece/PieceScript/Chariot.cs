using UnityEngine;

public class Chariot : PieceObject
{
    public override string GetName()
    {
        return "Chariot";
    }

    public override PieceMovement GetPieceMovement()
    {
        PieceMovement pm = new PieceMovement();
        pm.range[x, y + 1] = true;
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Chariot;
    }
}
