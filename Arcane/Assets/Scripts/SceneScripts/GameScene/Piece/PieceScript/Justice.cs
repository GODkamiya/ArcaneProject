using UnityEngine;

public class Justice : ActivePieceObject
{
    public override void ActiveEffect()
    {
        GameManager.singleton.phaseMachine.TransitionTo(new JusticePhase(this));
        
    }

    public override bool CanSpellActiveEffect()
    {
        return canActive;
    }

    public override string GetName()
    {
        return "正義";
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
        return PieceType.Justice;
    }

    public override string GetReverseEffectDescription()
    {
        return "盤の中心を基準に、点対象な位置に移動する。";
    }

    public override string GetUprightEffectDescription()
    {
        return "盤の中心を基準に、横に線対称な位置に移動する。";
    }
}
