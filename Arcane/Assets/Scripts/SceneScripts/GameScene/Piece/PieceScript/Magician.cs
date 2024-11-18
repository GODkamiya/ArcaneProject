using UnityEngine;

public class Magician : PieceObject
{
    public override string GetName()
    {
        return "Magician";
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Magician;
    }
}
