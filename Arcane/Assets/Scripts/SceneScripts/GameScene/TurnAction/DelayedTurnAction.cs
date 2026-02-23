using System;
using UnityEngine;

/// <summary>
/// 指定ターン経過後にアクションを実行するクラス
/// </summary>
public class DelayedTurnAction
{
    private int _remainingTurns { get; set; }
    private Action action;

    public bool IsFinished => _remainingTurns <= 0;

    public DelayedTurnAction(int lingerTurn, Action action)
    {
        this._remainingTurns = lingerTurn;
        this.action = action;
    }

    /// <summary>
    /// ターン経過処理。残りターンを減らし、0以下ならアクションを実行する。
    /// </summary>
    public void Tick()
    {
        _remainingTurns--;
        if (IsFinished)
        {
            action?.Invoke();
        }
    }
}
