using UnityEngine;

public class World : PieceObject
{
    public override string GetName()
    {
        return "World";
    }

    public override PieceMovement GetPieceMovement(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public override PieceType GetPieceType()
    {
        return PieceType.World;
    }
}
