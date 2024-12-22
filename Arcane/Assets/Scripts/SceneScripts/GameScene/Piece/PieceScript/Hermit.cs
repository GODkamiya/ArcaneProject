using UnityEngine;

public class Hermit : PieceObject
{
    public override string GetName()
    {
        return "Hermit";
    }

    public override PieceMovement GetPieceMovement(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Hermit;
    }
}
