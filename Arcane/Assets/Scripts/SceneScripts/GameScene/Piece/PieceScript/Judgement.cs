using UnityEngine;

public class Judgement : PieceObject
{
    public override string GetName()
    {
        return "Judgement";
    }

    public override PieceMovement GetPieceMovement()
    {
        PieceMovement pm = new PieceMovement();
        pm.range[x, y + 1] = true;
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Judgement;
    }
}
