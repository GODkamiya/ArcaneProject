using UnityEngine;

public class Chariot : PieceObject, IOnAttackEvent
{
    public override string GetName()
    {
        return "Chariot";
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
            }
        }
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Chariot;
    }

    public void OnAttack()
    {
        PieceMovement pieceMovement = gameObject.GetComponent<PieceObject>().GetPieceMovement();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (pieceMovement.range[i, j])
                {
                    GameObject target = BoardManager.singleton.onlinePieces[i, j];
                    if (target != null)
                    {
                        if(isReverse && isMine)continue;
                        target.GetComponent<PieceObject>().Death();
                    }
                }
            }
        }
    }
}
