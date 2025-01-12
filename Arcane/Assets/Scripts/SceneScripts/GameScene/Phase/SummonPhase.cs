using UnityEngine;

public class SummonPhase : IPhase
{
    public void Enter()
    {
        UIManager.singleton.ShowSummonPanel();
        PlayerClickHandler.singleton.clickAction = new SummonClickAction();
    }

    public void Exit()
    {
        UIManager.singleton.HideSummonPanel();
    }
}
