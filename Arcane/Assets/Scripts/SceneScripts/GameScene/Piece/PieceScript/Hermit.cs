using Fusion;
using TMPro;
using UnityEngine;

public class Hermit : ActivePieceObject
{
    /// <summary>
    /// 現在、透明状態かどうか
    /// </summary>
    public bool isTransparent = false;

    private NetworkObject previousTarget;
    private TurnActionManager turnActionManager;

    [VContainer.Inject]
    public void Construct(TurnActionManager turnActionManager)
    {
        this.turnActionManager = turnActionManager;
    }

    public override void ActiveEffect()
    {
        GameManager.singleton.phaseMachine.TransitionTo(new HermitPhase(this, previousTarget));
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void TransparentAlly_RPC(NetworkObject target)
    {
        canActive = false;
        previousTarget = target;
        // コマの所持者は見える状態がキープされる
        if (target.HasStateAuthority) return;

        target.gameObject.GetComponent<PieceObject>().SetEnable(false);
        target.gameObject.GetComponentInChildren<TextMeshPro>().text = "";
        void reset()
        {
            target.gameObject.GetComponent<PieceObject>().SetEnable(true);
            target.gameObject.GetComponentInChildren<TextMeshPro>().text = target.GetComponent<PieceObject>().GetName();
        }
        turnActionManager.Register(
                    new DelayedTurnAction(2, reset)
                );
    }

    public override bool CanSpellActiveEffect()
    {
        return canActive && GetIsReverse();
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
        return PieceType.Hermit;
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void ToggleTransparent_RPC()
    {
        isTransparent = !isTransparent;
    }
}
