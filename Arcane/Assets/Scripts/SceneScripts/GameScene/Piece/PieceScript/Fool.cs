using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fool : PieceObject
{
    public override string GetName()
    {
        return "Fool";
    }

    public override PieceMovement GetPieceMovement(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Fool;
    }
}
