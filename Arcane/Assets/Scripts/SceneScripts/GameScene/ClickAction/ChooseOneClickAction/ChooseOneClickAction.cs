using System;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class ChooseOneClickAction : IClickAction
{
    GameObject latestSelectPiece;

    List<TargetFilter> filterList;

    Action<GameObject> afterChooseAction;

    public ChooseOneClickAction(List<TargetFilter> filterList, Action<GameObject> afterChooseAction){
        this.filterList = filterList;
        this.afterChooseAction = afterChooseAction;
    }

    public void OnClickBoard(BoardBlock bb)
    {
    }

    public void OnClickPiece(GameObject pieceObject)
    {
        // 条件に当てはまらないピースを排除する
        foreach(TargetFilter filter in filterList){
            if(!filter.filterCondition(pieceObject))return;
        }

        latestSelectPiece = pieceObject;
    }

    public void OnPressButton(){
        if(latestSelectPiece == null) return;
        afterChooseAction(latestSelectPiece);
    }
}
