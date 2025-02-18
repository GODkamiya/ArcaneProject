using System;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class ChooseOneClickAction : IClickAction
{
    GameObject latestSelectPiece;

    List<TargetFilter> filterList;

    Action<GameObject> afterChooseAction;

    public ChooseOneClickAction(List<TargetFilter> filterList, Action<GameObject> afterChooseAction)
    {
        this.filterList = filterList;
        this.afterChooseAction = afterChooseAction;
    }

    public void OnClickBoard(BoardBlock bb)
    {
    }

    public void OnClickPiece(GameObject pieceObject)
    {
        // 条件に当てはまらないピースを排除する
        foreach (TargetFilter filter in filterList)
        {
            if (!filter.filterCondition(pieceObject)) return;
        }
        if(latestSelectPiece != null) ChangeOriginalColor(latestSelectPiece);
        latestSelectPiece = pieceObject;
        pieceObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void OnPressButton()
    {
        if (latestSelectPiece == null) return;
        ChangeOriginalColor(latestSelectPiece);
        afterChooseAction(latestSelectPiece);
    }

    private void ChangeOriginalColor(GameObject piece)
    {
        if (piece.GetComponent<PieceObject>().isKing)
        {
            piece.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            piece.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
