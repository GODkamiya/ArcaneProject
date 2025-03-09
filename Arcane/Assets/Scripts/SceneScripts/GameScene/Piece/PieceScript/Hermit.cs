using Fusion;
using TMPro;
using UnityEngine;

public class Hermit : ActivePieceObject
{
    public bool isTransparent = false;

    private NetworkObject previousTarget;

    public override void ActiveEffect()
    {
        GameManager.singleton.phaseMachine.TransitionTo(new HermitPhase(this,previousTarget));
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void TransparentAlly_RPC(NetworkObject target)
    {
        canActive = false;
        previousTarget = target;
        // コマの所持者は見える状態がキープされる
        if (target.HasStateAuthority) return;

        target.gameObject.GetComponent<Renderer>().enabled = false;
        target.gameObject.GetComponentInChildren<TextMeshPro>().text = "";
        void reset()
        {
            target.gameObject.GetComponent<Renderer>().enabled = true;
            target.gameObject.GetComponentInChildren<TextMeshPro>().text = target.GetComponent<PieceObject>().GetName();
        }
        GameManager.singleton.turnEndEvents.Add(
                    new TurnEndEvent(2, reset)
                );
    }

    public override bool CanSpellActiveEffect()
    {
        return canActive && isReverse;
    }

    public override string GetName()
    {
        return "隠者";
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
