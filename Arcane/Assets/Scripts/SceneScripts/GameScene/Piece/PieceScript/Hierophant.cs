using Fusion;
using UnityEngine;

public class Hierophant : ActivePieceObject
{
    public override void ActiveEffect()
    {
        if (GetIsReverse()) return;
        GameManager.singleton.phaseMachine.TransitionTo(new HierophantPhase(this));
    }

    public override string GetName()
    {
        return "教皇";
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
        return PieceType.Hierophant;
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void AddMovement_RPC(NetworkObject target)
    {
        canActive = false;
        var buff = new UDLRAddPieceMovement(1);
        target.GetComponent<PieceObject>().AddAddPieceMovement(buff);
        GameManager.singleton.turnEndEvents.Add(
            new TurnEndEvent(1, () => target.GetComponent<PieceObject>().RemoveAddPieceMovement(buff))
        );
        GameManager.singleton.phaseMachine.TransitionTo(new ActionPhase());
    }

    public override bool CanSpellActiveEffect()
    {
        if (GetIsReverse()) return false;
        return canActive;
    }

    public override string GetUprightEffectDescription()
    {
        return "このコマと同じ列、もしくは同じ行にいる味方コマ1体の移動範囲を、このターンの終わりまで上下左右+1する。";
    }

    public override string GetReverseEffectDescription()
    {
        return "このコマは効果を使用できなくなる。";
    }
}
