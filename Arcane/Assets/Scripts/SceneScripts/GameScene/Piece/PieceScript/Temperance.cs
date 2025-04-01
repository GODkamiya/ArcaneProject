using Fusion;
using UnityEngine;

public class Temperance : PieceObject
{
    public GameObject target;


    public override string GetName()
    {
        return "節制";
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
        return PieceType.Temperance;
    }

    public override string GetUprightEffectDescription()
    {
        return "このコマから一歩前にいるコマは移動も効果も使用できない。";
    }

    public override string GetReverseEffectDescription()
    {
        return "このコマから上下左右1の範囲内にいるコマは移動も効果も使用できない。";
    }
}
