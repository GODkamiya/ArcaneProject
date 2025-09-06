using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class Empress : PieceObject
{

    public GameObject selectedTarget;
    public List<GameObject> selectedTargetList;

    public override string GetName()
    {
        return "女帝";
    }

    public override PieceMovement GetPieceMovementOrigin(int baseX, int baseY)
    {
        PieceMovement pm = new PieceMovement();
        for (int addX = -1; addX < 2; addX++)
        {
            for (int addY = -1; addY < 2; addY++)
            {
                if (addX == 0 && addY == 0) continue;
                pm.AddRange(baseX + addX, baseY + addY);
            }
        }
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Empress;
    }

    public override string GetReverseEffectDescription()
    {
        return "範囲を縦横2の5x5範囲に拡大する。";
    }

    public override string GetUprightEffectDescription()
    {
        return "自分から王範囲にいる別の味方コマが倒れるとき、代わりにこのコマが倒れる。(敵のコマは、女帝の位置に移動する)";
    }
}
