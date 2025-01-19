using UnityEngine;

public class WheelOfFortunePhase : IPhase
{
    WheelOfFortune masterPiece;

    public WheelOfFortunePhase(WheelOfFortune masterPiece){
        this.masterPiece = masterPiece;
    }

    public void Enter()
    {
        var action = new WheelOfFortuneAction(masterPiece);
        PlayerClickHandler.singleton.clickAction = action;
        UIManager.singleton.ShowWheelOfFortunePanel(action);
    }

    public void Exit()
    {
        UIManager.singleton.HideWheelOfFortunePanel();
    }
}
