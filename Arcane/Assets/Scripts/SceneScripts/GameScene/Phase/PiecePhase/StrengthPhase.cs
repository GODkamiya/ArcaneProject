using System;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class StrengthPhase : IPhase
{
    Strength strength;
    public StrengthPhase(Strength strength)
    {
        this.strength = strength;
    }
    public void Enter()
    {
        List<TargetFilter> filters = new List<TargetFilter>(){
            new RangeFilter(strength.GetPieceMovement())
        };
        if (strength.isReverse)
        {
            filters.Add(new WithoutReverseFilter());
        }else{
            filters.Add(new WithoutAllyFilter());
            filters.Add(new ReverseFilter());
        }
        var action = new ChooseOneClickAction(filters, Effect);
        PlayerClickHandler.singleton.clickAction = action;
        UIManager.singleton.ShowChooseOneClickPanel(action);
    }
    private void Effect(GameObject target)
    {
        var piece = target.GetComponent<PieceObject>();
        piece.SetReverse_RPC(!piece.isReverse);
        strength.SetReverse_RPC(!strength.isReverse);
        GameManager.singleton.phaseMachine.TransitionTo(new ActionPhase());
    }

    public void Exit()
    {
        UIManager.singleton.HideChooseOneClickPanel();
    }
}
