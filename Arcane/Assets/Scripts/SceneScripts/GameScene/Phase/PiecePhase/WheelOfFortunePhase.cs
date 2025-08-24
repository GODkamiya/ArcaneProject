using System.Collections.Generic;
using UnityEngine;

public class WheelOfFortunePhase : IPhase
{
    WheelOfFortune masterPiece;

    public WheelOfFortunePhase(WheelOfFortune masterPiece)
    {
        this.masterPiece = masterPiece;
    }

    public void Enter()
    {
        // 正位置の実装
        if (!masterPiece.GetIsReverse())
        {
            List<TargetFilter> filterList = new List<TargetFilter>(){
                new WithoutSpecificObjectFilter(masterPiece.gameObject),
                new WithoutEnemyFilter(),
            };
            var action = new ChooseOneClickAction(
                filterList, (choosen) => masterPiece.ExchangePiece(choosen.GetComponent<PieceObject>(), masterPiece)
            );
            PlayerClickHandler.singleton.clickAction = action;
            UIManager.singleton.ShowChooseOneClickPanel(action);
        }
        // 逆位置の実装
        else
        {

            // 1体目の選択時
            void firstChoose(GameObject target)
            {
                List<TargetFilter> plusFilterList = new List<TargetFilter>(){
                    new WithoutEnemyFilter(),
                    new WithoutSpecificObjectFilter(target),
                };
                var secondAction = new ChooseOneClickAction(
                    plusFilterList, (targetB) => secondChoose(target, targetB)
                );
                PlayerClickHandler.singleton.clickAction = secondAction;
                UIManager.singleton.ShowChooseOneClickPanel(secondAction);
            }

            // 2体目の選択時
            void secondChoose(GameObject targetA, GameObject targetB)
            {
                masterPiece.ExchangePiece(targetA.GetComponent<PieceObject>(), targetB.GetComponent<PieceObject>());
            }


            List<TargetFilter> filterList = new List<TargetFilter>(){
                new WithoutEnemyFilter(),
            };
            var firstAction = new ChooseOneClickAction(
                filterList, firstChoose
            );
            PlayerClickHandler.singleton.clickAction = firstAction;
            UIManager.singleton.ShowChooseOneClickPanel(firstAction);
        }
    }

    public void Exit()
    {
        UIManager.singleton.HideChooseOneClickPanel();
    }
}
