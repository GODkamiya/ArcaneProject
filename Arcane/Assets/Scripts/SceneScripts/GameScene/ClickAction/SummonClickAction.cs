using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SummonClickAction : IClickAction, IClickHand
{
    private PieceType? latestSelectedPieceType;
    private GameObject putLocalPiece;
    private PieceType putLocalPieceType;
    private bool hasPut = false;
    private bool CanMovement(int x, int y)
    {
        if (BoardManager.singleton.onlinePieces[x, y]) return false;
        if(latestSelectedPieceType == PieceType.World) return true;
        if(latestSelectedPieceType == null && putLocalPieceType == PieceType.World) return true; //TODO コードをリファクタしたい
        foreach (GameObject gameObject in BoardManager.singleton.onlinePieces)
        {
            if(gameObject == null) continue;
            PieceObject pieceObject = gameObject.GetComponent<PieceObject>();
            if (pieceObject.GetPieceType() == PieceType.Star && pieceObject.isMine)
            {
                PieceMovement pm = pieceObject.GetPieceMovement();
                if (pm.range[x, y]) return true;
            }
        }
        if (y >= 3) return false;
        return true;
    }
    public void OnClickBoard(BoardBlock bb)
    {
        if (!CanMovement(bb.x, bb.y)) return;
        PieceType pt = latestSelectedPieceType ?? PieceType.Fool;
        // 既に置いてあるにも関わらず、新たなコマを召喚しようとする場合、既存のコマをしまい、新たなコマを召喚する
        if (hasPut && latestSelectedPieceType != null)
        {
            // 既存のコマを削除し、手札に戻す
            BoardManager.singleton.RemoveLocalPiece(putLocalPiece);
            PlayerObject po = GameManager.singleton.GetLocalPlayerObject();
            po.AddHand(putLocalPieceType);
            // 新たなコマの設置
            PutNewPiece(pt, bb.x, bb.y);
        }
        // 既に置いてあり、ただ盤をクリックしただけの場合は、コマを移動する。
        else if (hasPut)
        {
            BoardManager.singleton.RemoveLocalPiece(putLocalPiece);
            putLocalPiece = BoardManager.singleton.SetPiece(putLocalPieceType, bb.x, bb.y);
        }
        // まだコマを置いてない場合は、コマを設置する
        else if (latestSelectedPieceType != null)
        {
            PutNewPiece(pt, bb.x, bb.y);
        }
    }

    // 手札から新たなコマを召喚する
    private void PutNewPiece(PieceType pt, int x, int y)
    {
        PlayerObject po = GameManager.singleton.GetLocalPlayerObject();
        po.RemoveHand(pt);
        putLocalPiece = BoardManager.singleton.SetPiece(pt, x, y);
        putLocalPieceType = pt;
        latestSelectedPieceType = null;
        hasPut = true;
    }

    public void OnClickHand(PieceType pieceType)
    {
        latestSelectedPieceType = pieceType;
    }

    public void OnClickPiece(GameObject pieceObject)
    {
    }
}
