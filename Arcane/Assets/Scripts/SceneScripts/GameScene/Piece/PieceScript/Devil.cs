using UnityEngine;

public class Devil : PieceObject
{
    public override string GetName()
    {
        return "Devil";
    }

    public override PieceMovement GetPieceMovement(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Devil;
    }
}
