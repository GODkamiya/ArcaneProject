using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class Empress : ActivePieceObject
{
    
    public GameObject selectedTarget;
    public List<GameObject> selectedTargetList;
    public override void ActiveEffect()
    {
        GameManager.singleton.phaseMachine.TransitionTo(new EmpressPhase(this));
    }

    public override bool CanSpellActiveEffect()
    {
        return canActive;
    }

    public override string GetName()
    {
        return "女帝";
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
        return PieceType.Empress;
    }

    public override string GetReverseEffectDescription()
    {
        return "味方コマ1体を、次の相手のターンの終わりまで倒れない状態にする。同じコマを2回指定することはできない。";
    }

    public override string GetUprightEffectDescription()
    {
        return "敵コマ1体を、次の相手のターンの終わりまで攻撃できない状態にする。同じ敵コマを2連続で指定することはできない。";
    }
}
