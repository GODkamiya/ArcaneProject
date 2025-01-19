using UnityEngine;

public class ActionPhase : IPhase
{
    public void Enter()
    {
        PlayerClickHandler.singleton.clickAction = new PieceMoveClickAction();
    }

    public void Exit()
    {
        PlayerClickHandler.singleton.clickAction = new NoneAction();
        UIManager.singleton.HideAbilityButton();
        BoardManager.singleton.ClearMovement();
    }
}
