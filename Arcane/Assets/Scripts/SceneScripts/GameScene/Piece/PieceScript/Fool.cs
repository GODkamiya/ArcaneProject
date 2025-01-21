using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fool : PieceObject
{
    public override string GetName()
    {
        return "Fool";
    }

    public override PieceMovement GetPieceMovementOrigin()
    {
        PieceMovement pm = new PieceMovement();
        pm.AddRange(x,y + 1);
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Fool;
    }
}
