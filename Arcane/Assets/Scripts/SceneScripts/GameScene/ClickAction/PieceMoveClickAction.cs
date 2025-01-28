using UnityEngine;

public class PieceMoveClickAction : IClickAction
{
    private GameObject latestPiece;
    private PieceMovement latestMove;
    public void OnClickBoard(BoardBlock bb)
    {
        if (latestPiece == null) return;
        if (!latestMove.range[bb.x, bb.y]) return;
        if (BoardManager.singleton.onlinePieces[bb.x, bb.y] != null && BoardManager.singleton.onlinePieces[bb.x, bb.y].GetComponent<PieceObject>().isMine) return;
        latestPiece.GetComponent<PieceObject>().SetPosition(bb.x, bb.y, true);
        BoardManager.singleton.ClearMovement();
        GameManager.singleton.TurnEnd();
    }

    public void OnClickPiece(GameObject pieceObject)
    {
        PieceObject piece = pieceObject.GetComponent<PieceObject>();
        if (!piece.isMine) return;
        latestPiece = pieceObject;
        PieceMovement move = piece.GetPieceMovement();
        latestMove = move;
        BoardManager.singleton.ShowMovement(move);
        if (piece is ActivePieceObject activePiece && activePiece.canActive)
        {
            UIManager.singleton.ShowAbilityButton(() => activePiece.ActiveEffect());
        }
        else
        {
            UIManager.singleton.HideAbilityButton();
        }
    }
}
