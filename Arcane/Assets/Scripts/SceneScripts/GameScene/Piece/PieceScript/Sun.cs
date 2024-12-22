using UnityEngine;

public class Sun : PieceObject
{
    public override string GetName()
    {
        return "Sun";
    }

    public override PieceMovement GetPieceMovement()
    {
        PieceMovement pm = new PieceMovement();
        pm.range[x, y + 1] = true;
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Sun;
    }
}
