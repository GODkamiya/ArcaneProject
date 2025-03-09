using System.Collections.Generic;
using UnityEngine;

public class EmpressPhase : IPhase
{
    Empress masterPiece;
    GameObject selectedTarget;
    public EmpressPhase(Empress masterPiece)
    {
        this.masterPiece = masterPiece;
        selectedTarget = masterPiece.selectedTarget;
    }
    public void Enter()
    {
        List<TargetFilter> filters = new List<TargetFilter>(){
            new WithoutSpecificObjectFilter(selectedTarget)
        };
        if (!masterPiece.isReverse)
        {
            filters.Add(new WithoutAllyFilter());
        }
        var action = new ChooseOneClickAction(filters, Effect);
        PlayerClickHandler.singleton.clickAction = action;
        UIManager.singleton.ShowChooseOneClickPanel(action);
    }
    private void Effect(GameObject target)
    {
        var piece = target.GetComponent<PieceObject>();
        if (piece.isMine)
        {
            //逆位置の実装
        }
        else
        {
            masterPiece.selectedTarget = target;
            piece.SetAttackable(false);
        }
        masterPiece.canActive = false;
        GameManager.singleton.phaseMachine.TransitionTo(new ActionPhase());
    }

    public void Exit()
    {
        UIManager.singleton.HideChooseOneClickPanel();
    }
}
