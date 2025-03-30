using UnityEngine;

public class Star : PieceObject
{
    public override string GetName()
    {
        return "星";
    }

    public override PieceMovement GetPieceMovementOrigin(int baseX,int baseY)
    {
        PieceMovement pm = new PieceMovement();
        for (int addX = -1; addX < 2; addX++)
        {
            for (int addY = -1; addY < 2; addY++)
            {
                if (addX == 0 && addY == 0) continue;
                pm.AddRange(baseX + addX, baseY + addY);
                // 逆位置の場合に、移動範囲が増加する
                if (isReverse && (addX == 0 || addY == 0))
                {
                    pm.AddRange(baseX + addX * 2, baseY + addY * 2);
                }
            }
        }
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Star;
    }

    public override string GetReverseEffectDescription()
    {
        return "移動範囲が広がる。";
    }

    public override string GetUprightEffectDescription()
    {
        return "このコマの移動範囲内に、コマを召喚できる。";
    }
}
