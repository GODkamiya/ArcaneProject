using UnityEngine;

public class Magician : ActivePieceObject
{
    public int counter = 1;
    public override void ActiveEffect()
    {
        GameManager.singleton.phaseMachine.TransitionTo(new MagicianPhase(this));
    }

    public override bool CanSpellActiveEffect()
    {
        if(counter == 0 ) return false;
        return canActive;
    }

    public override string GetName()
    {
        return "魔術師";
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
        return PieceType.Magician;
    }
}
