using UnityEngine;

public class SummonClickAction : IClickAction, IClickHand
{
    private GameObject latestSelectPiece;
    private PieceType? latestSelectHandPieceType;
    public void OnClickBoard(BoardBlock bb)
    {
        if(bb.y >= 3)return;
        if (latestSelectPiece != null)
        {
            BoardManager.singleton.SetPiece(latestSelectPiece.GetComponent<PieceObject>().GetPieceType(), bb.x, bb.y);
            BoardManager.singleton.RemoveLocalPiece(latestSelectPiece);
            latestSelectPiece = null;
        }
        else if (latestSelectHandPieceType != null)
        {
            PieceType pt = latestSelectHandPieceType ?? PieceType.Fool; // TODO nullの対処
            PlayerObject po = GameManager.singleton.GetLocalPlayerObject();
            po.RemoveHand(pt);
            BoardManager.singleton.SetPiece(pt, bb.x, bb.y);
            latestSelectHandPieceType = null;
        }
    }

    public void OnClickHand(PieceType pieceType)
    {
        latestSelectHandPieceType = pieceType;
        latestSelectPiece = null;
    }

    public void OnClickPiece(GameObject pieceObject)
    {
        latestSelectPiece = pieceObject;
        latestSelectHandPieceType = null;
    }
}
