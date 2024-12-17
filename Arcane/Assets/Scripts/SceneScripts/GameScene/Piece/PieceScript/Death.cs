using UnityEngine;

public class Death : PieceObject
{
    public override string GetName()
    {
        return "Death";
    }

    public override PieceMovement GetPieceMovement()
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Death;
    }
}
