using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fool : PieceObject
{
    public override PieceMovement GetPieceMovementOrigin(int baseX,int baseY)
    {
        PieceMovement pm = new PieceMovement();
        pm.AddRange(baseX,baseY + 1);
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Fool;
    }
}
