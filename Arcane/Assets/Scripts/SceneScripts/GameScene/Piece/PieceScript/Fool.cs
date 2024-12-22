using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fool : PieceObject
{
    public override string GetName()
    {
        return "Fool";
    }

    public override PieceMovement GetPieceMovement()
    {
        PieceMovement pm = new PieceMovement();
        pm.range[x, y - 1] = true;
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Fool;
    }
}
