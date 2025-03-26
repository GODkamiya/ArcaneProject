using UnityEngine;

public class Lovers : PieceObject
{
    public override string GetName()
    {
        return "恋人";
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
        return PieceType.Lovers;
    }

    public override string GetReverseEffectDescription()
    {
        return "もう一方のコマを、このコマの移動範囲内の好きなところに移動させる。";
    }

    public override string GetUprightEffectDescription()
    {
        return "このコマは2体同時に召喚する。片方のコマが移動する場合、もう片方のコマも移動する。また、片方のコマが倒れたとき、もう片方のコマも倒れる。";
    }
}
