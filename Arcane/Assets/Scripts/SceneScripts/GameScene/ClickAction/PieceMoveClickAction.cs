using UnityEngine;

public class PieceMoveClickAction : IClickAction
{
    private GameObject latestPiece;
    private PieceMovement latestMove;
    public void OnClickBoard(BoardBlock bb)
    {
        if(latestPiece == null)return;
        if(!latestMove.range[bb.x, bb.y])return;
        BoardManager.singleton.RemovePieceOnBoard(latestPiece.GetComponent<PieceObject>().x, latestPiece.GetComponent<PieceObject>().y);
        latestPiece.GetComponent<PieceObject>().SetPosition(bb.x, bb.y);
        BoardManager.singleton.ClearMovement();
        GameManager.singleton.TurnEnd();
    }

    public void OnClickPiece(GameObject pieceObject)
    {
        PieceObject piece = pieceObject.GetComponent<PieceObject>();
        if(!piece.isMine)return;
        latestPiece = pieceObject;
        PieceMovement move = piece.GetPieceMovement();
        latestMove = move;
        BoardManager.singleton.ShowMovement(move);
    }
}
