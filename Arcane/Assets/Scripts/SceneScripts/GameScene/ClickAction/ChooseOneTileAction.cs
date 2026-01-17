using System;
using UnityEngine;

public class ChooseOneTileAction : IClickAction
{

    BoardBlock latestBlock;

    Action<BoardBlock> action;

    PieceMovement movementRange;

    public ChooseOneTileAction(Action<BoardBlock> action, PieceMovement movementRange)
    {
        this.action = action;
        this.movementRange = movementRange;
    }

    public void OnClickBoard(BoardBlock bb)
    {
        if (!movementRange.CanMovement(bb.x, bb.y)) return;
        if (BoardManager.singleton.onlinePieces[bb.x, bb.y] != null) return;
        if (latestBlock != null) latestBlock.GetComponent<Renderer>().material.color = Color.white;
        latestBlock = bb;
        bb.GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void OnClickPiece(GameObject pieceObject)
    {
        PieceObject target = pieceObject.GetComponent<PieceObject>();
        BoardBlock bb = BoardManager.singleton.GetTile(target.x, target.y).GetComponent<BoardBlock>();
        OnClickBoard(bb);
    }

    public void OnPressButton()
    {
        if (latestBlock == null) return;
        latestBlock.GetComponent<Renderer>().material.color = Color.white;
        action(latestBlock);
    }
    public void OnPressButton(string buttonName)
    {
        if (latestBlock == null) return;
        latestBlock.GetComponent<Renderer>().material.color = Color.white;
        action(latestBlock);
    }
}
