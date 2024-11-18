using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fool : PieceObject
{
    public override string GetName()
    {
        return "Fool";
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Fool;
    }
}
