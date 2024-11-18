using UnityEngine;

public class Emperor : PieceObject
{
    public override string GetName()
    {
        return "Emperor";
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Emperor;
    }
}
