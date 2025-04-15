using System;
using System.Linq;
using UnityEngine;

public class InitialSummonClickAction : IClickAction, IClickHand
{
    private GameObject latestSelectPiece;
    private PieceType? latestSelectHandPieceType;
    private LocalBoardManager localBoardManager;

    public InitialSummonClickAction(LocalBoardManager localBoardManager)
    {
        this.localBoardManager = localBoardManager;
    }

    public void OnClickBoard(BoardBlock bb)
    {
        if (bb.y >= 3) return;

        // コマと重なる場合、早期リターンで置けなくする
        if (localBoardManager.HasPiece(bb.x, bb.y)) return;
        // 既に置いてあるコマを選択中の場合、そのコマを移動させる
        if (latestSelectPiece != null)
        {
            GameObject putLocalPiece = localBoardManager.SetPiece(latestSelectPiece.GetComponent<PieceObject>().GetPieceType(), bb.x, bb.y);
            // TODO 吊るされた男の専用処理をなんとかしたい
            if(latestSelectPiece.GetComponent<PieceObject>().GetPieceType() == PieceType.HangedMan){
                putLocalPiece.GetComponent<HangedMan>().SetPretender(latestSelectPiece.GetComponent<HangedMan>().GetPretender() ?? PieceType.HangedMan); // TODO nullの対処
            }
            localBoardManager.RemovePiece(latestSelectPiece);
            latestSelectPiece = null;
        }
        // 手札を選択中の場合、手札から新たにコマを配置する
        else if (latestSelectHandPieceType != null)
        {
            PieceType pt = latestSelectHandPieceType ?? PieceType.Fool; // TODO nullの対処
            PlayerObject po = GameManager.singleton.GetLocalPlayerObject();
            po.RemoveHand(pt);
            GameObject putLocalPiece = localBoardManager.SetPiece(pt, bb.x, bb.y);
            latestSelectHandPieceType = null;
            // TODO そもそも、ここに吊るされた男の専用処理があるのは何とかするべき
            if (pt == PieceType.HangedMan)
            {
                void SetPretender(PieceType selectedPretender)
                {
                    putLocalPiece.GetComponent<HangedMan>().SetPretender(selectedPretender);
                }
                UIManager.singleton.ShowPieceListPanel(Enum.GetValues(typeof(PieceType)).Cast<PieceType>().ToList(), SetPretender);
            }
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
