using System.Collections.Generic;
using UnityEngine;

public class HierophantPhase : IPhase
{
    Hierophant masterPiece;

    public HierophantPhase(Hierophant masterPiece){
        this.masterPiece = masterPiece;
    }

    public void Enter()
    {
        List<TargetFilter> filterList = new List<TargetFilter>(){
            new WithoutEnemyFilter()
        };
        var action = new ChooseOneClickAction(
            filterList,(choosen) => masterPiece.AddMovement(choosen)
        );
        PlayerClickHandler.singleton.clickAction = action;
        UIManager.singleton.ShowChooseOneClickPanel(action);
    }

    public void Exit()
    {
        UIManager.singleton.HideChooseOneClickPanel();
    }
}
