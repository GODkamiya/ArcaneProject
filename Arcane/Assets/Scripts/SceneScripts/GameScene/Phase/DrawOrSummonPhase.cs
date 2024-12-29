using UnityEngine;

public class DrawOrSummonPhase : IPhase
{
    public void Enter()
    {
        UIManager.singleton.ShowDrawOrSummonPanel();
    }

    public void Exit()
    {
        UIManager.singleton.HideDrawOrSummonPanel();
    }
}
