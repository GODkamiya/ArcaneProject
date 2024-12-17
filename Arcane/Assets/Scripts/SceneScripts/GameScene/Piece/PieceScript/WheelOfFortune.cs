using UnityEngine;

public class WheelOfFortune : PieceObject
{
    public override string GetName()
    {
        return "WheelOfFortune";
    }

    public override PieceMovement GetPieceMovement()
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.WheelOfFortune;
    }
}
