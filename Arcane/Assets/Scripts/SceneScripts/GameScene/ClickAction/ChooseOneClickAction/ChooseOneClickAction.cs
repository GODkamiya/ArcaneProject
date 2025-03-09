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
        // 悪魔の対象になっている場合は選べない
        for(int i = -2 ; i <= 2; i++){
            for(int j = -2; j <= 2; j++){
                PieceObject pieceData = pieceObject.GetComponent<PieceObject>();
                int x = pieceData.x;
                int y = pieceData.y;
                if(x + i < 0 || x + i > 9 || y + j < 0 || y + j > 9)continue;
                if(BoardManager.singleton.onlinePieces[x+i,y+j] != null){
                    PieceObject targetData = BoardManager.singleton.onlinePieces[x+i,y+j].GetComponent<PieceObject>();
                    if(!targetData.isMine && targetData.GetPieceType() == PieceType.Devil)return;
                }
            }
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
