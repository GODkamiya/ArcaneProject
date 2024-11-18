using UnityEngine;

public class Hierophant : PieceObject
{
    public override string GetName()
    {
        return "Hierophant";
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Hierophant;
    }
}
