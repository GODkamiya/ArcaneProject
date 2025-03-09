using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class TemperancePhase : IPhase
{
    Temperance masterPiece;

    public TemperancePhase(Temperance masterPiece)
    {
        this.masterPiece = masterPiece;
    }

    public void Enter()
    {
        List<TargetFilter> filters = new List<TargetFilter>(){
            new WithoutSpecificObjectFilter(masterPiece.gameObject)
        };
        void afterChoose(GameObject target)
        {
            masterPiece.Effect_RPC(target.GetComponent<NetworkObject>());
            GameManager.singleton.phaseMachine.TransitionTo(new ActionPhase());
        }
        var action = new ChooseOneClickAction(filters, afterChoose);
        PlayerClickHandler.singleton.clickAction = action;
        UIManager.singleton.ShowChooseOneClickPanel(action);
    }

    public void Exit()
    {
        UIManager.singleton.HideChooseOneClickPanel();
    }
}
