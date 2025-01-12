using UnityEngine;

public class InitialSummonPhase : IPhase
{
    public void Enter()
    {
        UIManager.singleton.ShowInitSummonPanel();
        PlayerClickHandler.singleton.clickAction = new InitialSummonClickAction();
    }

    public void Exit()
    {
        UIManager.singleton.HideInitSummonPanel();
        PlayerClickHandler.singleton.clickAction = new NoneAction();
    }
}
