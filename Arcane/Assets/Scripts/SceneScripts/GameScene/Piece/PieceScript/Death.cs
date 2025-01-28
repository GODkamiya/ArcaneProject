using UnityEngine;

public class Death : PieceObject
{
    public override string GetName()
    {
        return "Death";
    }

    public override PieceMovement GetPieceMovementOrigin()
    {
        PieceMovement pm = new PieceMovement();
        for (int addY = 1; addY < 10; addY++)
        {
            pm.AddRange(x, y + addY);
        }
        // 逆位置の場合に、移動範囲が増加する
        if(isReverse){
            for (int addX = -1; addX < 2; addX++)
            {
                for (int addY = -1; addY < 2; addY++)
                {
                    if (addX == 0 && addY == 0) continue;
                    pm.AddRange(x + addX, y + addY);
                }
            }
        }
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Death;
    }
}
