using UnityEngine;

public class Emperor : PieceObject
{
    public override string GetName()
    {
        return "Emperor";
    }

    public override PieceMovement GetPieceMovement(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Emperor;
    }
}
