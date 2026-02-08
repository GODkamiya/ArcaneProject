using System.Collections.Generic;
using VContainer;

/// <summary>
/// ターン経過で実行されるアクションを管理するシステム
/// </summary>
public class TurnActionManager
{
    private readonly List<DelayedTurnAction> _actions = new List<DelayedTurnAction>();

    /// <summary>
    /// 遅延実行アクションを登録する
    /// </summary>
    public void Register(DelayedTurnAction action)
    {
        _actions.Add(action);
    }

    /// <summary>
    /// ターン終了時の処理。アクションのカウントを進め、完了したものを削除する。
    /// </summary>
    public void OnTurnEnd()
    {
        for (int i = _actions.Count - 1; i >= 0; i--)
        {
            var action = _actions[i];
            action.Tick();
            if (action.IsFinished)
            {
                _actions.RemoveAt(i);
            }
        }
    }
}
