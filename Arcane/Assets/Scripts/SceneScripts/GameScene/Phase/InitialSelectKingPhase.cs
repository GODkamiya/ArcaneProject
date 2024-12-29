using UnityEngine;

public class InitialSelectKingPhase : IPhase
{
    public void Enter()
    {
        UIManager.singleton.ShowInitSelectKingPanel();
        PlayerClickHandler.singleton.clickAction = new KingSelectAction();
    }

    public void Exit()
    {
        UIManager.singleton.HideInitSelectKingPanel();
        PlayerClickHandler.singleton.clickAction = new NoneAction();
    }
}
