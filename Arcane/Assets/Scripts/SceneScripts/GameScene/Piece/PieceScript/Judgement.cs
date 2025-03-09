using UnityEngine;

public class Judgement : PieceObject, IOnAttackEvent
{
    public override string GetName()
    {
        return "審判";
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
        return PieceType.Judgement;
    }

    public void OnAttack(int newX, int newY,PieceObject newTarget)
    {
        if(!isMine) return;
        GameManager.singleton.GetLocalPlayerObject().AddHand(newTarget.GetPieceType());
    }
}
