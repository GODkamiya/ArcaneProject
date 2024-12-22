using UnityEngine;

public class WheelOfFortune : PieceObject
{
    public override string GetName()
    {
        return "WheelOfFortune";
    }

    public override PieceMovement GetPieceMovement(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.WheelOfFortune;
    }
}
