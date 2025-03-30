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
            new SpecificYFilter(masterPiece.y),
            new WithoutAllyFilter()
        };

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
            (tile) => AfterChooseTile(target, tile), GetEffectRange(target)
        );
    }

    // コマとテレポート先、どちらも選ばれ終わった後、実際に移動させる
    private void AfterChooseTile(GameObject target, BoardBlock bb)
    {
        masterPiece.MovePieceByEffect(target.GetComponent<NetworkObject>(), bb);
    }


    private PieceMovement GetEffectRange(GameObject target)
    {
        var pm = new PieceMovement();
        for (int addX = -1; addX < 2; addX++)
        {
            for (int addY = -1; addY < 2; addY++)
            {
                if(addX == 0 && addY == 0)continue;
                pm.AddRange(target.GetComponent<PieceObject>().x + addX, target.GetComponent<PieceObject>().y + addY);
            }
        }
        return pm;
    }

    public void Exit()
    {
        UIManager.singleton.HideChooseOneClickPanel();
    }
}
