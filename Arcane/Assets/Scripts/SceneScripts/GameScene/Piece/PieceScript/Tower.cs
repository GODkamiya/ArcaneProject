using Fusion;
using UnityEngine;

public class Tower : ActivePieceObject
{
    public override void ActiveEffect()
    {
        if (!isReverse) return;
        canActive = false;
        Deny_RPC();
        GameManager.singleton.phaseMachine.TransitionTo(new ActionPhase());
    }

    public override bool CanSpellActiveEffect()
    {
        if(!isReverse) return false;
        return canActive;
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void Deny_RPC()
    {
        Death();
    }

    public override string GetName()
    {
        return "塔";
    }

    public override PieceMovement GetPieceMovementOrigin(int baseX, int baseY)
    {
        PieceMovement pm = new PieceMovement();
        //移動しないよ
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Tower;
    }

    public override string GetReverseEffectDescription()
    {
        return "自爆することができる。";
    }

    public override string GetUprightEffectDescription()
    {
        return "このコマが倒れたとき、このコマを中心にした5×5の範囲内にいるすべてのコマを取る。";
    }
}
