using UnityEngine;

public class HighPriestess : PieceObject
{
    public override string GetName()
    {
        return "HighPriestess";
    }

    public override PieceMovement GetPieceMovement()
    {
        PieceMovement pm = new PieceMovement();
        pm.range[x, y + 1] = true;
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.HighPriestess;
    }
}
