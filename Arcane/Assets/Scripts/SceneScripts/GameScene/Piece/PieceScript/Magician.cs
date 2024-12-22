using UnityEngine;

public class Magician : PieceObject
{
    public override string GetName()
    {
        return "Magician";
    }

    public override PieceMovement GetPieceMovement()
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Magician;
    }
}
