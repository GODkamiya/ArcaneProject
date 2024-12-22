using UnityEngine;

public class Hierophant : PieceObject
{
    public override string GetName()
    {
        return "Hierophant";
    }

    public override PieceMovement GetPieceMovement()
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Hierophant;
    }
}
