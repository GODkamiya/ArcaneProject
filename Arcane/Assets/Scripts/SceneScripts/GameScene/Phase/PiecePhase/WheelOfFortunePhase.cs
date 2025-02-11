using System.Collections.Generic;

public class WheelOfFortunePhase : IPhase
{
    WheelOfFortune masterPiece;

    public WheelOfFortunePhase(WheelOfFortune masterPiece)
    {
        this.masterPiece = masterPiece;
    }

    public void Enter()
    {
        List<TargetFilter> filterList = new List<TargetFilter>(){
            new WithoutSpecificObjectFilter(masterPiece.gameObject),
            new WithoutEnemyFilter(),
        };
        var action = new ChooseOneClickAction(
            filterList,(choosen) => masterPiece.ExchangePiece(choosen.GetComponent<PieceObject>())
        );
        PlayerClickHandler.singleton.clickAction = action;
        UIManager.singleton.ShowChooseOneClickPanel(action);
    }

    public void Exit()
    {
        UIManager.singleton.HideChooseOneClickPanel();
    }
}
