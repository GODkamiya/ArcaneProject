using UnityEngine;
using VContainer;

public class PhaseMachine
{
    private IPhase currentPhase;
    private IObjectResolver _container;

    public PhaseMachine(IObjectResolver container)
    {
        _container = container;
    }

    public void Initialize(IPhase initialPhase){
        _container.Inject(initialPhase);
        this.currentPhase = initialPhase;
        currentPhase.Enter();
    }
    public void TransitionTo(IPhase nextPhase){
        if(currentPhase is GameEndPhase)return;
        currentPhase.Exit();
        _container.Inject(nextPhase);
        this.currentPhase = nextPhase;
        currentPhase.Enter();
    }
}
