using UnityEngine;

public class InitialSummonPhase : IPhase
{
    public void Enter()
    {
        PlayerObject po = GameManager.singleton.GetLocalPlayerObject();
        po.SetDeck();
        for (int i = 0; i < 5; i++)
        {
            po.DrawDeck();
        }
        UIManager.singleton.ShowInitSummonPanel();
        PlayerClickHandler.singleton.clickAction = new InitialSummonClickAction();
    }

    public void Exit()
    {
        UIManager.singleton.HideInitSummonPanel();
        PlayerClickHandler.singleton.clickAction = new NoneAction();
    }
}
