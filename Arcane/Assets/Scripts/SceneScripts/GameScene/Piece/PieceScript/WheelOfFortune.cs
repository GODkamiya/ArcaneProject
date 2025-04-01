using Fusion;
using UnityEngine;

public class WheelOfFortune : ActivePieceObject
{
    public override void ActiveEffect()
    {
        GameManager.singleton.phaseMachine.TransitionTo(new WheelOfFortunePhase(this));
    }

    public override string GetName()
    {
        return "運命の輪";
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
        return PieceType.WheelOfFortune;
    }

    public void ExchangePiece(PieceObject targetA,PieceObject targetB){
        canActive = false;
        int targetAX = targetA.x;
        int targetAY = targetA.y;
        targetA.SetPosition(targetB.x, targetB.y,false,false);
        targetB.SetPosition(targetAX, targetAY,false,false);
        GameManager.singleton.phaseMachine.TransitionTo(new ActionPhase());
    }

    public override bool CanSpellActiveEffect()
    {
        return canActive;
    }

    public override string GetUprightEffectDescription()
    {
        return "指定した味方コマ1体とこのコマの位置を入れ替える。";
    }

    public override string GetReverseEffectDescription()
    {
        return "指定した味方コマ2体の位置を入れ替える。";
    }
}
