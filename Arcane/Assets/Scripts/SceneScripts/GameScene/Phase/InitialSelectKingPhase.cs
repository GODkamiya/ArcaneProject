using UnityEngine;

public class InitialSelectKingPhase : IPhase
{
    private LocalBoardManager localBoardManager;
    private PreparationTurn preparationTurn;

    public InitialSelectKingPhase(LocalBoardManager localBoardManager,PreparationTurn preparationTurn)
    {
        this.localBoardManager = localBoardManager;
        this.preparationTurn = preparationTurn;
    }
    public void Enter()
    {
        void doneSelectKing()
        {
            if (!localBoardManager.HasKing()) return;
            preparationTurn.SetBoardManager(localBoardManager);
            localBoardManager.SetKing();
            GameManager.singleton.phaseMachine.TransitionTo(new WaitPhase());
            GameManager.singleton.SwitchIsReady_Rpc(GameManager.singleton.HasStateAuthority ? 0 : 1);
        }
        UIManager.singleton.ShowInitSelectKingPanel(doneSelectKing);
        PlayerClickHandler.singleton.clickAction = new KingSelectAction(localBoardManager);
    }

    public void Exit()
    {
        UIManager.singleton.HideInitSelectKingPanel();
        PlayerClickHandler.singleton.clickAction = new NoneAction();
    }
}
