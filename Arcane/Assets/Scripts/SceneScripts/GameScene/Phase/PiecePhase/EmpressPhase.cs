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
        else
        {
            foreach (GameObject targetType in masterPiece.selectedTargetList)
            {
                filters.Add(new WithoutSpecificObjectFilter(targetType));
            }
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
            masterPiece.selectedTargetList.Add(target);
            piece.SetImmortality(true);
            GameManager.singleton.turnEndEvents.Add(
                    new TurnEndEvent(2, () => piece.SetImmortality(false))
                );
        }
        else
        {
            masterPiece.selectedTarget = target;
            piece.SetAttackable(false);
            GameManager.singleton.turnEndEvents.Add(
                    new TurnEndEvent(2, ()=> piece.SetAttackable(true))
                );
        }
        masterPiece.canActive = false;
        GameManager.singleton.phaseMachine.TransitionTo(new ActionPhase());
    }

    public void Exit()
    {
        UIManager.singleton.HideChooseOneClickPanel();
    }
}
