using UnityEngine;

public class Emperor : PieceObject
{
    public override string GetName()
    {
        return "Emperor";
    }

    public override PieceMovement GetPieceMovement()
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Emperor;
    }
}
