using UnityEngine;

public class Tower : PieceObject
{
    public override string GetName()
    {
        return "Tower";
    }

    public override PieceMovement GetPieceMovement()
    {
        PieceMovement pm = new PieceMovement();
        //移動しないよ
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Tower;
    }
}
