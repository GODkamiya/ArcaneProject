using Fusion;
using UnityEngine;

public class Strength : ActivePieceObject
{
    public override void ActiveEffect()
    {
        GameManager.singleton.phaseMachine.TransitionTo(new StrengthPhase(this));
    }
    public override bool CanSpellActiveEffect()
    {
        return canActive;
    }

    public override string GetName()
    {
        return "力";
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
        return PieceType.Strength;
    }

    public override string GetUprightEffectDescription()
    {
        return "自分の移動範囲内にいる逆位置の敵コマ1体を正位置にする。その後、このコマは逆位置になる。";
    }

    public override string GetReverseEffectDescription()
    {
        return "自分の移動範囲内にいる正位置のコマ1体を逆位置にする。その後、このコマは正位置になる。";
    }
}
