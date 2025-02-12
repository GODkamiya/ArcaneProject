using UnityEngine;

public class InitialSummonClickAction : IClickAction, IClickHand
{
    private GameObject latestSelectPiece;
    private PieceType? latestSelectHandPieceType;
    private LocalBoardManager localBoardManager;

    public InitialSummonClickAction(LocalBoardManager localBoardManager){
        this.localBoardManager=localBoardManager;
    }

    public void OnClickBoard(BoardBlock bb)
    {
        if(bb.y >= 3)return;

        // コマと重なる場合、早期リターンで置けなくする
        if(localBoardManager.HasPiece(bb.x, bb.y)) return;
        // 既に置いてあるコマを選択中の場合、そのコマを移動させる
        if (latestSelectPiece != null)
        {
            localBoardManager.SetPiece(latestSelectPiece.GetComponent<PieceObject>().GetPieceType(), bb.x, bb.y);
            localBoardManager.RemovePiece(latestSelectPiece);
            latestSelectPiece = null;
        }
        // 手札を選択中の場合、手札から新たにコマを配置する
        else if (latestSelectHandPieceType != null)
        {
            PieceType pt = latestSelectHandPieceType ?? PieceType.Fool; // TODO nullの対処
            PlayerObject po = GameManager.singleton.GetLocalPlayerObject();
            po.RemoveHand(pt);
            localBoardManager.SetPiece(pt,bb.x,bb.y);
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
