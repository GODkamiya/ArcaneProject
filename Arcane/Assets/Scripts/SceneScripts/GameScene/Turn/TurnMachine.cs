using UnityEngine;

/// <summary>
/// ターンを管理するクラス
/// </summary>
public class TurnMachine
{
    private ITurn currentTurn;
    public void Initialize(ITurn initialTurn)
    {
        this.currentTurn = initialTurn;
        currentTurn.Enter();
    }
    public void TransitionTo(ITurn nextTurn)
    {
        currentTurn.Exit();
        this.currentTurn = nextTurn;
        currentTurn.Enter();
    }

    public ITurn GetCurrentTurn() => currentTurn;
}
