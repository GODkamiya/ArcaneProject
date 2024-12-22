using UnityEngine;

public class Hermit : PieceObject
{
    public override string GetName()
    {
        return "Hermit";
    }

    public override PieceMovement GetPieceMovement()
    {
        PieceMovement pm = new PieceMovement();
        pm.range[x, y + 1] = true;
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Hermit;
    }
}
