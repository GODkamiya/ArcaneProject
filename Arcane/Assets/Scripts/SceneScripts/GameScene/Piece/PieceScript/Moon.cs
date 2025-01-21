using UnityEngine;

public class Moon : PieceObject
{
    public override string GetName()
    {
        return "Moon";
    }

    public override PieceMovement GetPieceMovementOrigin()
    {
        PieceMovement pm = new PieceMovement();
        for (int addX = -1; addX < 2; addX++)
        {
            for (int addY = -1; addY < 2; addY++)
            {
                if(addX == 0 && addY == 0)continue;
                pm.AddRange(x + addX, y + addY);
            }
        }
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Moon;
    }
}
