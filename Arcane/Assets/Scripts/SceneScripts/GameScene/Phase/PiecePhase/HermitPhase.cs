using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;

public class HermitPhase : IPhase
{
    Hermit masterPiece;

    NetworkObject previousTarget;

    public HermitPhase(Hermit masterPiece,NetworkObject previousTarget)
    {
        this.masterPiece = masterPiece;
        this.previousTarget = previousTarget;
    }

    public void Enter()
    {
        void transparentAlly(GameObject target)
        {
            masterPiece.TransparentAlly_RPC(target.GetComponent<NetworkObject>());
            GameManager.singleton.phaseMachine.TransitionTo(new ActionPhase());
        }

        List<TargetFilter> filters = new List<TargetFilter>(){
            new WithoutEnemyFilter(),
            new WithoutSpecificObjectFilter(masterPiece.gameObject)
        };
        if(previousTarget != null){
            filters.Add(new WithoutSpecificObjectFilter(previousTarget.gameObject));
        }

        var action = new ChooseOneClickAction(filters, transparentAlly);

        PlayerClickHandler.singleton.clickAction = action;
        UIManager.singleton.ShowChooseOneClickPanel(action);
    }

    public void Exit()
    {
        UIManager.singleton.HideChooseOneClickPanel();
    }
}
