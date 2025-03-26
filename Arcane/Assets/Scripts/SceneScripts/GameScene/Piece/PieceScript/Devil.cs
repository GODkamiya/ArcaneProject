using UnityEngine;

public class Devil : PieceObject
{
    public override string GetName()
    {
        return "悪魔";
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
        return PieceType.Devil;
    }

    public override string GetReverseEffectDescription()
    {
        return "このコマを中心にした5×5の範囲内にいる敵コマは効果の発動ができない。";
    }

    public override string GetUprightEffectDescription()
    {
        return "このコマを中心にした5×5の範囲内にいる味方コマは、敵コマの効果の対象にならない。";
    }
}
