using UnityEngine;

public class PieceMoveClickAction : IClickAction
{
    private GameObject latestPiece;
    private PieceMovement latestMove;
    public void OnClickBoard(BoardBlock bb)
    {
        if (latestPiece == null) return;
        if (!latestMove.range[bb.x, bb.y]) return;
        if (BoardManager.singleton.onlinePieces[bb.x, bb.y] != null)
        {
            if (!latestPiece.GetComponent<PieceObject>().isAttackable) return;
            if (BoardManager.singleton.onlinePieces[bb.x, bb.y].GetComponent<PieceObject>().isImmortality) return;
            if (BoardManager.singleton.onlinePieces[bb.x, bb.y].GetComponent<PieceObject>().GetPieceType() == PieceType.Emperor)
            {
                if (!BoardManager.singleton.onlinePieces[bb.x, bb.y].GetComponent<PieceObject>().isReverse && latestPiece.GetComponent<PieceObject>().isReverse) return;
                if (BoardManager.singleton.onlinePieces[bb.x, bb.y].GetComponent<PieceObject>().isReverse && !latestPiece.GetComponent<PieceObject>().isReverse) return;
            }
        }
        if (!(latestPiece.GetComponent<PieceObject>().GetPieceType() == PieceType.Judgement && latestPiece.GetComponent<PieceObject>().isReverse))
        {
            if (BoardManager.singleton.onlinePieces[bb.x, bb.y] != null && BoardManager.singleton.onlinePieces[bb.x, bb.y].GetComponent<PieceObject>().isMine) return;
        }
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
        if (piece.temperance != null) return;
        if (piece.GetPieceType() == PieceType.Temperance && ((Temperance)piece).target != null) return;

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
        if (piece is ActivePieceObject activePiece && activePiece.CanSpellActiveEffectMaster() && !activePiece.isSickness)
        {
            UIManager.singleton.ShowAbilityButton(() => activePiece.ActiveEffect());
        }
        else
        {
            UIManager.singleton.HideAbilityButton();
        }
    }
}
