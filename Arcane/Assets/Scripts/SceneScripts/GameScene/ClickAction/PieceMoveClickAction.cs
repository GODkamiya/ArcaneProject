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
        if (latestPiece.GetComponent<PieceObject>().isKing)
        {
            latestPiece.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            latestPiece.GetComponent<Renderer>().material.color = Color.white;
        }
        latestPiece.GetComponent<PieceObject>().SetPosition(bb.x, bb.y, true);
        BoardManager.singleton.ClearMovement();
        GameManager.singleton.TurnEnd();
    }

    public void OnClickPiece(GameObject pieceObject)
    {
        PieceObject piece = pieceObject.GetComponent<PieceObject>();
        if (!piece.isMine) return;
        if (piece.isSickness) return;
        if (latestPiece != null)
        {
            if (latestPiece.GetComponent<PieceObject>().isKing)
            {
                latestPiece.GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                latestPiece.GetComponent<Renderer>().material.color = Color.white;
            }
        }
        latestPiece = pieceObject;
        pieceObject.GetComponent<Renderer>().material.color = Color.cyan;
        PieceMovement move = piece.GetPieceMovement();
        latestMove = move;
        BoardManager.singleton.ShowMovement(move);
        if (piece is ActivePieceObject activePiece && activePiece.CanSpellActiveEffect() && !activePiece.isSickness)
        {
            UIManager.singleton.ShowAbilityButton(() => activePiece.ActiveEffect());
        }
        else
        {
            UIManager.singleton.HideAbilityButton();
        }
    }
}
