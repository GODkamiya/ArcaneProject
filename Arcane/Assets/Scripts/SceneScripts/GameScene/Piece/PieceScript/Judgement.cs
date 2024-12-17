using UnityEngine;

public class Judgement : PieceObject
{
    public override string GetName()
    {
        return "Judgement";
    }

    public override PieceMovement GetPieceMovement()
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Judgement;
    }
}
