using UnityEngine;

public class Chariot : PieceObject, IOnAttackEvent
{
    public override string GetName()
    {
        return "Chariot";
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
            }
        }
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Chariot;
    }

    public void OnAttack(int newX,int newY,PieceObject target)
    {
        // 移動予定の位置からの移動範囲を求める
        PieceMovement pieceMovement = gameObject.GetComponent<PieceObject>().GetPieceMovement(newX,newY);
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (pieceMovement.range[i, j])
                {
                    GameObject newTarget = BoardManager.singleton.onlinePieces[i, j];
                    if (newTarget != null)
                    {
                        if(isReverse && newTarget.GetComponent<PieceObject>().isMine == isMine)continue;
                        newTarget.GetComponent<PieceObject>().Death();
                    }
                }
            }
        }
    }
}
