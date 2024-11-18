using UnityEngine;

public class World : PieceObject
{
    public override string GetName()
    {
        return "World";
    }

    public override PieceType GetPieceType()
    {
        return PieceType.World;
    }
}
