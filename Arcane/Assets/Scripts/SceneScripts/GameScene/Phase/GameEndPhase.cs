using UnityEngine;

public class GameEndPhase : IPhase
{
    bool is1pWin;
    public GameEndPhase(bool is1pWin){
        this.is1pWin=is1pWin;
    }
    public void Enter()
    {
        UIManager.singleton.ShowGameEndPanel(is1pWin);
    }

    public void Exit()
    {
    }
}
