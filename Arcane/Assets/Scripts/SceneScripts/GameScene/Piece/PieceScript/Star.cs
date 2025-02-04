using UnityEngine;

public class Star : PieceObject
{
    public override string GetName()
    {
        return "Star";
    }

    public override PieceMovement GetPieceMovementOrigin()
    {
        PieceMovement pm = new PieceMovement();
        for (int addX = -1; addX < 2; addX++)
        {
            for (int addY = -1; addY < 2; addY++)
            {
                if (addX == 0 && addY == 0) continue;
                pm.AddRange(x + addX, y + addY);
                // 逆位置の場合に、移動範囲が増加する
                if (isReverse && (addX == 0 || addY == 0))
                {
                    pm.AddRange(x + addX * 2, y + addY * 2);
                }
            }
        }
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Star;
    }
}
