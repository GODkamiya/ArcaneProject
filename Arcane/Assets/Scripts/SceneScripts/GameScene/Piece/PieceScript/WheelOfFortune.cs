using UnityEngine;

public class WheelOfFortune : PieceObject
{
    public override string GetName()
    {
        return "WheelOfFortune";
    }

    public override PieceMovement GetPieceMovement()
    {
        PieceMovement pm = new PieceMovement();
        pm.range[x, y + 1] = true;
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.WheelOfFortune;
    }
}
