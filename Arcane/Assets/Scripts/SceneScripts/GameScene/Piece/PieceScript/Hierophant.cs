using Fusion;
using UnityEngine;
using VContainer;

public class Hierophant : ActivePieceObject
{
    private GameConfig _gameconfig;
    [Inject]
    public void Construct(GameConfig gameConfig)
    {
        _gameconfig = gameConfig;
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
        var buff = new UDLRAddPieceMovement(1,_gameconfig);
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
}
