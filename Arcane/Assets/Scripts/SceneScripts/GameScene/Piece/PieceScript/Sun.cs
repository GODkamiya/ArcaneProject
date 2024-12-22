using UnityEngine;

public class Sun : PieceObject
{
    public override string GetName()
    {
        return "Sun";
    }

    public override PieceMovement GetPieceMovement(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Sun;
    }
}
