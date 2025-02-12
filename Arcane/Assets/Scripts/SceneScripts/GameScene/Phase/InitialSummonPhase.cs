using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class InitialSummonPhase : IPhase
{
    private PreparationTurn preparationTurn;

    public InitialSummonPhase(PreparationTurn preparationTurn){
        this.preparationTurn=preparationTurn;
    }

    public void Enter()
    {
        // プレイヤーのデッキを構成し、5枚ドローさせる。
        PlayerObject po = GameManager.singleton.GetLocalPlayerObject();
        po.SetDeck();
        for (int i = 0; i < 5; i++)
        {
            po.DrawDeck();
        }

        LocalBoardManager localBoardManager = new LocalBoardManager();
        PlayerClickHandler.singleton.clickAction = new InitialSummonClickAction(localBoardManager);
        void doneSummon(){
            if (localBoardManager.GetPieceCount() != 5) return;
            GameManager.singleton.phaseMachine.TransitionTo(new InitialSelectKingPhase(localBoardManager,preparationTurn));
        }
        UIManager.singleton.ShowInitSummonPanel(doneSummon);
    }

    public void Exit()
    {
        UIManager.singleton.HideInitSummonPanel();
        PlayerClickHandler.singleton.clickAction = new NoneAction();
    }
}
