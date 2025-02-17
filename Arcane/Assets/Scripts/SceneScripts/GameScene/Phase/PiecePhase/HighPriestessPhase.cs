using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;

public class HighPriestessPhase : IPhase
{

    HighPriestess masterPiece;

    public HighPriestessPhase(HighPriestess masterPiece)
    {
        this.masterPiece = masterPiece;
    }

    public void Enter()
    {
        void AfterChooseTile(GameObject target, BoardBlock bb)
        {
            masterPiece.MovePieceByEffect(target.GetComponent<NetworkObject>(), bb);
        }
        void AfterChooseTarget(GameObject target)
        {
            UIManager.singleton.HideChooseOneClickPanel();
            var tileAction = new ChooseOneTileAction(
                (tile) => AfterChooseTile(target, tile), GetEffectRange()
            );
            PlayerClickHandler.singleton.clickAction = tileAction;
            UIManager.singleton.ShowChooseOneTilePanel(tileAction);
        }

        List<TargetFilter> filterList = new List<TargetFilter>(){
            new WithoutAllyFilter(),
            new RangeFilter(GetEffectRange())
        };
        var action = new ChooseOneClickAction(
            filterList, AfterChooseTarget
        );
        PlayerClickHandler.singleton.clickAction = action;
        UIManager.singleton.ShowChooseOneClickPanel(action);
    }

    private PieceMovement GetEffectRange()
    {
        var range = new PieceMovement();
        for (int i = -2; i <= 2; i++)
        {
            for (int j = -2; j <= 2; j++)
            {
                if (i == 0 && j == 0) continue;
                range.AddRange(masterPiece.x + i, masterPiece.y + j);
            }
        }
        return range;
    }

    public void Exit()
    {
        UIManager.singleton.HideChooseOneClickPanel();
    }
}
