using UnityEngine;

public class HangedMan : PieceObject
{
    public override string GetName()
    {
        return "HangedMan";
    }

    public override PieceMovement GetPieceMovementOrigin(int baseX,int baseY)
    {
        PieceMovement pm = new PieceMovement();
        for (int addX = -1; addX < 2; addX++)
        {
            for (int addY = -1; addY < 2; addY++)
            {
                if(addX == 0 && addY == 0)continue;
                pm.AddRange(baseX + addX, baseY + addY);
            }
        }
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.HangedMan;
    }
}
