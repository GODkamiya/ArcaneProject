using UnityEngine;

public class Magician : PieceObject
{
    public override string GetName()
    {
        return "Magician";
    }

    public override PieceMovement GetPieceMovement(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Magician;
    }
}
