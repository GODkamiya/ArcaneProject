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
            new WithoutEnemyFilter(),
            new WithoutReverseFilter(),
        };
        if (!strength.isReverse)
        {
            filters.Add(new RangeFilter(strength.GetPieceMovement()));
        }
        var action = new ChooseOneClickAction(filters, Effect);
        PlayerClickHandler.singleton.clickAction = action;
        UIManager.singleton.ShowChooseOneClickPanel(action);
    }
    private void Effect(GameObject target)
    {
        strength.Effect_RPC(target.GetComponent<NetworkObject>());
        GameManager.singleton.phaseMachine.TransitionTo(new ActionPhase());
    }

    public void Exit()
    {
        UIManager.singleton.HideChooseOneClickPanel();
    }
}
