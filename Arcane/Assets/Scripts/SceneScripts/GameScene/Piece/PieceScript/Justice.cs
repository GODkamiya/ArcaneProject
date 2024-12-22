using UnityEngine;

public class Justice : PieceObject
{
    public override string GetName()
    {
        return "Justice";
    }

    public override PieceMovement GetPieceMovement(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Justice;
    }
}
