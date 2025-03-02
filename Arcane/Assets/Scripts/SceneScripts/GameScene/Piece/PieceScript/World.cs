using UnityEngine;

public class World : PieceObject, IOnAttackEvent
{
    public override string GetName()
    {
        return "World";
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
        return PieceType.World;
    }

    public void OnAttack(int newX, int newY,PieceObject target)
    {
        // 既存のコマを削除し、手札に戻す
        Death();
        PlayerObject po = GameManager.singleton.GetLocalPlayerObject();
        if (isMine)
        {
            po.AddHand(PieceType.World);
        }
    }
}
