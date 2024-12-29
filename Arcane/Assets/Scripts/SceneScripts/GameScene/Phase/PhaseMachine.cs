using UnityEngine;

public class PhaseMachine
{
    private IPhase currentPhase;
    public void Initialize(IPhase initialPhase){
        this.currentPhase = initialPhase;
        currentPhase.Enter();
    }
    public void TransitionTo(IPhase nextPhase){
        if(currentPhase is GameEndPhase)return;
        currentPhase.Exit();
        this.currentPhase = nextPhase;
        currentPhase.Enter();
    }
}
