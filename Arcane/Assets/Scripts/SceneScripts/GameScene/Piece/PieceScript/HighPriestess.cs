using Fusion;
using UnityEngine;

public class HighPriestess : ActivePieceObject
{
    public override void ActiveEffect()
    {
        GameManager.singleton.phaseMachine.TransitionTo(new HighPriestessPhase(this));
    }

    public void MovePieceByEffect(NetworkObject target, BoardBlock bb)
    {
        canActive = false;
        MovePieceByEffect_Rpc(target,bb.x,bb.y);
        GameManager.singleton.phaseMachine.TransitionTo(new ActionPhase());
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void MovePieceByEffect_Rpc(NetworkObject target, int x, int y)
    {
        PieceObject po = target.GetComponent<PieceObject>();
        if(po.isMine){
            if(!isMine){
                x = 9-x;
                y = 9-y;
            }
            po.SetPosition(x, y, false,false);
        }
    }

    public override string GetName()
    {
        return "女教皇";
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
        return PieceType.HighPriestess;
    }

    public override bool CanSpellActiveEffect()
    {
        return canActive;
    }

    public override string GetUprightEffectDescription()
    {
        return "このコマを中心にした5×5の範囲内にいる敵コマ1体を、その範囲内の好きな位置に移動させる。";
    }

    public override string GetReverseEffectDescription()
    {
        return "";
    }
}
