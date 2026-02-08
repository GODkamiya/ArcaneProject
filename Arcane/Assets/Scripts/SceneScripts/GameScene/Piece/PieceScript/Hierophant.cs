using Fusion;
using UnityEngine;

public class Hierophant : ActivePieceObject
{
    private TurnActionManager turnActionManager;

    [VContainer.Inject]
    public void Construct(TurnActionManager turnActionManager)
    {
        this.turnActionManager = turnActionManager;
    }

    public override void ActiveEffect()
    {
        if (GetIsReverse()) return;
        GameManager.singleton.phaseMachine.TransitionTo(new HierophantPhase(this));
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
        turnActionManager.Register(
            new DelayedTurnAction(1, () => target.GetComponent<PieceObject>().RemoveAddPieceMovement(buff))
        );
        GameManager.singleton.phaseMachine.TransitionTo(new ActionPhase());
    }

    public override bool CanSpellActiveEffect()
    {
        if (GetIsReverse()) return false;
        return canActive;
    }
}
