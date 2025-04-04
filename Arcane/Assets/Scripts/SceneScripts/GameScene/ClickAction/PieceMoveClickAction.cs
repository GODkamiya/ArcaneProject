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
        latestPiece.GetComponent<PieceObject>().SetPosition(bb.x, bb.y, true,false);
        BoardManager.singleton.ClearMovement();
        GameManager.singleton.TurnEnd();
    }

    public void OnClickPiece(GameObject pieceObject)
    {
        PieceObject piece = pieceObject.GetComponent<PieceObject>();
        if (!piece.isMine) return;
        if (piece.isSickness) return;
        int poX = pieceObject.GetComponent<PieceObject>().x;
        int poY = pieceObject.GetComponent<PieceObject>().y;

        bool CheckPiece(int x, int y, bool? reverse)
        {
            var piece = BoardManager.singleton.onlinePieces[x, y];
            //近くに節制がいるとき
            if (piece == null) return false; 
            if (piece.GetComponent<PieceObject>().isMine) return false;
            if (piece.GetComponent<PieceObject>().GetPieceType() != PieceType.Temperance) return false;
            //発動条件を満たしていれば移動・効果を封じる
            return reverse == null || piece.GetComponent<PieceObject>().isReverse;
        }

        if (CheckPiece(poX, poY + 1, null) || CheckPiece(poX, poY - 1, true) || CheckPiece(poX - 1, poY, true) || CheckPiece(poX + 1, poY, true)) return;

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
