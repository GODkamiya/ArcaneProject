using UnityEngine;

public class Emperor : ActivePieceObject
{
    public override void ActiveEffect()
    {
        canActive = false;
        isReverse = false;
        GameManager.singleton.phaseMachine.TransitionTo(new ActionPhase());
    }

    public override bool CanSpellActiveEffect()
    {
        if(!isReverse) return false;
        return canActive;
    }

    public override string GetName()
    {
        return "皇帝";
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
        return PieceType.Emperor;
    }

    public override string GetReverseEffectDescription()
    {
        return "";
    }

    public override string GetUprightEffectDescription()
    {
        return "";
    }
}
