using UnityEngine;

/// <summary>
/// 準備ターン
/// 5体召喚→王選択が完了するまでを担当
/// </summary>
public class PreparationTurn : ITurn
{
    private LocalBoardManager boardManager;

    public void Enter()
    {
        GameManager.singleton.phaseMachine.Initialize(new InitialSummonPhase(this));
    }

    public void Exit()
    {
    }

    public void SetBoardManager(LocalBoardManager boardManager) => this.boardManager = boardManager;
    public LocalBoardManager GetLocalBoardManager() => boardManager;
}
