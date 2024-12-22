using UnityEngine;

public class Moon : PieceObject
{
    public override string GetName()
    {
        return "Moon";
    }

    public override PieceMovement GetPieceMovement(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Moon;
    }
}
