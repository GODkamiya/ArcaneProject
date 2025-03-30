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

    public override string GetReverseEffectDescription()
    {
        return "味方コマを倒せるようになる。味方コマも同様に、倒すことで手札に加えられる。";
    }

    public override string GetUprightEffectDescription()
    {
        return "敵コマを倒したとき、その敵コマを手札に加える。";
    }

    public void OnAttack(int newX, int newY,PieceObject newTarget)
    {
        if(!isMine) return;
        GameManager.singleton.GetLocalPlayerObject().AddHand(newTarget.GetPieceType());
    }
}
