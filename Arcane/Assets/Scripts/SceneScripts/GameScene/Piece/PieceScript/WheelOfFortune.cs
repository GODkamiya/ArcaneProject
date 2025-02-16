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
        return "WheelOfFortune";
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
        targetA.SetPosition(targetB.x, targetB.y,false);
        targetB.SetPosition(targetAX, targetAY,false);
        GameManager.singleton.phaseMachine.TransitionTo(new ActionPhase());
    }

}
