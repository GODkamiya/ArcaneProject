using UnityEngine;
using VContainer;

public class ActionPhase : IPhase
{
    [Inject] private GameConfig _config = default!;
    public void Enter()
    {
        PlayerClickHandler.singleton.clickAction = new PieceMoveClickAction(_config);
    }

    public void Exit()
    {
        PlayerClickHandler.singleton.clickAction = new NoneAction();
        UIManager.singleton.HideAbilityButton();
        BoardManager.singleton.ClearMovement();
    }
}
