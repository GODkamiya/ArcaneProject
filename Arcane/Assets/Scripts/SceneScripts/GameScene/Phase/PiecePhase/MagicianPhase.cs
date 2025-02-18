using System.Collections.Generic;
using UnityEngine;

public class MagicianPhase : IPhase
{
    Magician magician;
    public MagicianPhase(Magician magician)
    {
        this.magician = magician;
    }
    public void Enter()
    {
        List<TargetFilter> filters = new List<TargetFilter>() { };
        if (magician.isReverse)
        {
            filters.Add(new WithoutReverseFilter());
        }
        else
        {
            filters.Add(new ReverseFilter());
        }
        var action = new ChooseOneClickAction(filters, Effect);
        PlayerClickHandler.singleton.clickAction = action;
        UIManager.singleton.ShowChooseOneClickPanel(action);
    }
    private void Effect(GameObject target)
    {
        var piece = target.GetComponent<PieceObject>();
        piece.SetReverse(!piece.isReverse);
        magician.counter = 0;
        GameManager.singleton.phaseMachine.TransitionTo(new ActionPhase());
    }
    public void Exit()
    {
        UIManager.singleton.HideChooseOneClickPanel();
    }
}
