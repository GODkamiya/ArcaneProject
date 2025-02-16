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
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void Deny_RPC()
    {
        Death();
    }

    public override string GetName()
    {
        return "Tower";
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
}
