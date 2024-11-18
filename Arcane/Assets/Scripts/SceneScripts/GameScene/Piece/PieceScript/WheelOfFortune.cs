using UnityEngine;

public class WheelOfFortune : PieceObject
{
    public override string GetName()
    {
        return "WheelOfFortune";
    }

    public override PieceType GetPieceType()
    {
        return PieceType.WheelOfFortune;
    }
}
