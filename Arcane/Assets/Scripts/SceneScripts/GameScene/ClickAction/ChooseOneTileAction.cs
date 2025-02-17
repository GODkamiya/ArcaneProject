using System;
using UnityEngine;

public class ChooseOneTileAction : IClickAction
{

    BoardBlock latestBlock;

    Action<BoardBlock> action;

    PieceMovement movementRange;

    public ChooseOneTileAction(Action<BoardBlock> action,PieceMovement movementRange){
        this.action = action;
        this.movementRange = movementRange;
    }

    public void OnClickBoard(BoardBlock bb)
    {
        if(!movementRange.CanMovement(bb.x,bb.y))return;
        if(BoardManager.singleton.onlinePieces[bb.x,bb.y] != null) return;
        latestBlock = bb;
    }

    public void OnClickPiece(GameObject pieceObject)
    {
    }

    public void OnPressButton(){
        if(latestBlock == null) return;
        action(latestBlock);
    }
}
