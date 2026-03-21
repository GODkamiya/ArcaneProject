using UnityEngine;
using VContainer;

/// <summary>
/// ターンを管理するクラス
/// </summary>
public class TurnMachine
{
    private ITurn currentTurn;
    private IObjectResolver _container;

    public TurnMachine(IObjectResolver container)
    {
        _container = container;
    }

    public void Initialize(ITurn initialTurn)
    {
        _container.Inject(initialTurn);
        this.currentTurn = initialTurn;
        currentTurn.Enter();
    }
    public void TransitionTo(ITurn nextTurn)
    {
        currentTurn.Exit();
        _container.Inject(nextTurn);
        this.currentTurn = nextTurn;
        currentTurn.Enter();
    }

    public ITurn GetCurrentTurn() => currentTurn;
}
