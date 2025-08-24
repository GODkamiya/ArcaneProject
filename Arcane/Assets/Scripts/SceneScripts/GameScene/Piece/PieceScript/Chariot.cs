using UnityEngine;

public class Chariot : PieceObject, IOnAttackEvent
{
    public override string GetName()
    {
        return "戦車";
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
        return PieceType.Chariot;
    }

    public override string GetReverseEffectDescription()
    {
        return "正位置の効果で倒す対象が敵のコマだけになる。";
    }

    public override string GetUprightEffectDescription()
    {
        return "このコマが敵コマを倒した後、その位置から移動範囲内にいる全てのコマを倒す。";
    }

    public void OnAttack(int newX, int newY, PieceObject target)
    {
        // 移動予定の位置からの移動範囲を求める
        PieceMovement pieceMovement = gameObject.GetComponent<PieceObject>().GetPieceMovement(newX, newY);
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (pieceMovement.range[i, j])
                {
                    GameObject newTarget = BoardManager.singleton.onlinePieces[i, j];
                    if (newTarget != null)
                    {
                        if (GetIsReverse() && newTarget.GetComponent<PieceObject>().isMine == isMine) continue;
                        newTarget.GetComponent<PieceObject>().Death();
                    }
                }
            }
        }
    }
}
