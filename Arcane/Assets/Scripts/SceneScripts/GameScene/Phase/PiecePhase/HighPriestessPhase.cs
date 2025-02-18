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
        // コマを1体選ぶ処理への移行
        var action = CreateChooseOneClickAction();
        PlayerClickHandler.singleton.clickAction = action;
        UIManager.singleton.ShowChooseOneClickPanel(action);
    }

    // コマを一体選ぶアクションの生成
    private ChooseOneClickAction CreateChooseOneClickAction()
    {
        // フィルターの定義
        List<TargetFilter> filterList = new List<TargetFilter>(){
            new RangeFilter(GetEffectRange())
        };

        // 逆位置の場合、味方も指定できる
        if(!masterPiece.isReverse){
            filterList.Add(new WithoutAllyFilter());
        }

        return new ChooseOneClickAction(
            filterList, AfterChooseTarget
        );
    }

    // コマを選んだあと、テレポート先を選ぶ処理への移行
    private void AfterChooseTarget(GameObject target)
    {
        UIManager.singleton.HideChooseOneClickPanel();
        var action = CreateChooseOneTileAction(target);
        PlayerClickHandler.singleton.clickAction = action;
        UIManager.singleton.ShowChooseOneTilePanel(action);
    }

    // テレポート先を選ぶアクションの生成
    private ChooseOneTileAction CreateChooseOneTileAction(GameObject target)
    {
        return new ChooseOneTileAction(
            (tile) => AfterChooseTile(target, tile), GetEffectRange()
        );
    }

    // コマとテレポート先、どちらも選ばれ終わった後、実際に移動させる
    private void AfterChooseTile(GameObject target, BoardBlock bb)
    {
        masterPiece.MovePieceByEffect(target.GetComponent<NetworkObject>(), bb);
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
