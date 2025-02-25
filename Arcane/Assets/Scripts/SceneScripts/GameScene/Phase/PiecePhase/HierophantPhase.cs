using System.Collections.Generic;
using Fusion;

public class HierophantPhase : IPhase
{
    Hierophant masterPiece;

    public HierophantPhase(Hierophant masterPiece){
        this.masterPiece = masterPiece;
    }

    public void Enter()
    {
        List<TargetFilter> filterList = new List<TargetFilter>(){
            new WithoutEnemyFilter(),
            new OrFilter(
            new SpecificXFilter(masterPiece.x),
            new SpecificYFilter(masterPiece.y)
            ),
        };
        var action = new ChooseOneClickAction(
            filterList,(choosen) => masterPiece.AddMovement_RPC(choosen.GetComponent<NetworkObject>())
        );
        PlayerClickHandler.singleton.clickAction = action;
        UIManager.singleton.ShowChooseOneClickPanel(action);
    }

    public void Exit()
    {
        UIManager.singleton.HideChooseOneClickPanel();
    }
}
