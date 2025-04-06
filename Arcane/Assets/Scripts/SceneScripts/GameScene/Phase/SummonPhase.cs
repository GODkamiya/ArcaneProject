using UnityEngine;

public class SummonPhase : IPhase
{
    public void Enter()
    {
        LocalBoardManager localBoardManager = new LocalBoardManager();
        PlayerClickHandler.singleton.clickAction = new SummonClickAction(localBoardManager);

        void afterSummon()
        {
            // ちゃんと召喚したかの確認
            if (localBoardManager.GetPieceCount() == 0) return;
            // コマの共有
            BoardManager.singleton.AsyncPiece(GameManager.singleton.Runner, false, localBoardManager); // TODO : LocalBoardManagerが空だよ！
            GameManager.singleton.ChangeActionPhaseUI_RPC();
            GameManager.singleton.phaseMachine.TransitionTo(new ActionPhase());
        }
        UIManager.singleton.ShowSummonPanel(afterSummon);
    }

    public void Exit()
    {
        UIManager.singleton.HideSummonPanel();
    }
}
