using UnityEngine;

public class Hierophant : PieceObject
{
    public override string GetName()
    {
        return "Hierophant";
    }

    public override PieceMovement GetPieceMovement()
    {
        PieceMovement pm = new PieceMovement();
        pm.range[x, y + 1] = true;
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Hierophant;
    }
}
