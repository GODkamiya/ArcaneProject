using UnityEngine;

public class Chariot : PieceObject
{
    public override string GetName()
    {
        return "Chariot";
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Chariot;
    }
}
