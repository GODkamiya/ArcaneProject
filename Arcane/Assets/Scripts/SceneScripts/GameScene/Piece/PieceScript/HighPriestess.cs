using UnityEngine;

public class HighPriestess : PieceObject
{
    public override string GetName()
    {
        return "HighPriestess";
    }

    public override PieceMovement GetPieceMovement(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.HighPriestess;
    }
}
