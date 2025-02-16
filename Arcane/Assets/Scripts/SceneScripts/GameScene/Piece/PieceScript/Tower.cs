using UnityEngine;

public class Tower : PieceObject
{
    public override string GetName()
    {
        return "Tower";
    }

    public override PieceMovement GetPieceMovementOrigin(int baseX,int baseY)
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
