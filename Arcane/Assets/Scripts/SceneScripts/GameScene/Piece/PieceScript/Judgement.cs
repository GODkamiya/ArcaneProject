using UnityEngine;

public class Judgement : PieceObject
{
    public override string GetName()
    {
        return "Judgement";
    }

    public override PieceMovement GetPieceMovement(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Judgement;
    }
}
