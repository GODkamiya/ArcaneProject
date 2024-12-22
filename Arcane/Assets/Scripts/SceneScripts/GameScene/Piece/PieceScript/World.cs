using UnityEngine;

public class World : PieceObject
{
    public override string GetName()
    {
        return "World";
    }

    public override PieceMovement GetPieceMovement()
    {
        PieceMovement pm = new PieceMovement();
        pm.range[x, y + 1] = true;
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.World;
    }
}
