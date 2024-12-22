using UnityEngine;

public class Chariot : PieceObject
{
    public override string GetName()
    {
        return "Chariot";
    }

    public override PieceMovement GetPieceMovement()
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Chariot;
    }
}
