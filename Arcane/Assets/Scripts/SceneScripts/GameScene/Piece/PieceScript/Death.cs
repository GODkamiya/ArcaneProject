using UnityEngine;

public class Death : PieceObject
{
    public override string GetName()
    {
        return "Death";
    }

    public override PieceMovement GetPieceMovement()
    {
        PieceMovement pm = new PieceMovement();
        for(int addY = 1; addY < 10;addY++){
            pm.AddRange(x,y + addY);
        }
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Death;
    }
}
