using UnityEngine;

public class Temperance : PieceObject
{
    public override string GetName()
    {
        return "Temperance";
    }

    public override PieceMovement GetPieceMovement(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Temperance;
    }
}
