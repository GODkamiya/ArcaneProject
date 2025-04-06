using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PieceMoveClickAction : IClickAction
{
    private GameObject latestPiece;
    private PieceMovement latestMove;
    public void OnClickBoard(BoardBlock bb)
    {
        if (latestPiece == null) return;
        if (!latestMove.range[bb.x, bb.y]) return;
        var clickedPiece = BoardManager.singleton.onlinePieces[bb.x, bb.y];

        // 攻撃判定・特殊条件
        if (clickedPiece != null)
        {
            var targetObj = clickedPiece.GetComponent<PieceObject>();

            // 攻撃できないならリターン
            if (!latestPiece.GetComponent<PieceObject>().isAttackable) return;

            // 不死身の駒は倒せない
            if (targetObj.isImmortality) return;

            // 皇帝の特殊ルール
            if (targetObj.GetPieceType() == PieceType.Emperor)
            {
                bool attackerRev = latestPiece.GetComponent<PieceObject>().isReverse;
                bool targetRev = targetObj.isReverse;
                if (attackerRev != targetRev) return;
            }

            // 審判以外の駒が、自分の駒を攻撃しようとしたらリターン
            bool isJudgementReverse = latestPiece.GetComponent<PieceObject>().GetPieceType() == PieceType.Judgement && latestPiece.GetComponent<PieceObject>().isReverse;
            if (!isJudgementReverse && targetObj.isMine) return;
        }

        // 女帝判定処理
        int posX = bb.x;
        int posY = bb.y;
        List<int> empressPos = FindNearbyEmpress(bb);
        if (empressPos != null)
        {
            posX = empressPos[0];
            posY = empressPos[1];
        }

        // 駒を移動させる処理
        latestPiece.GetComponent<Renderer>().material.color = latestPiece.GetComponent<PieceObject>().isKing ? Color.red : Color.white;
        latestPiece.GetComponent<PieceObject>().SetPosition(posX, posY, true, false);
        BoardManager.singleton.ClearMovement();
        GameManager.singleton.TurnEnd();
    }
    /// <summary>
    /// 女帝の身代わり処理
    /// </summary>
    /// <param name="center"></param>
    /// <returns></returns>
    private List<int> FindNearbyEmpress(BoardBlock center)
    {
        var clickedPiece = BoardManager.singleton.onlinePieces[center.x, center.y];
        if (clickedPiece == null) return null;

        var centerObj = clickedPiece.GetComponent<PieceObject>();
        // 敵駒以外はスキップ
        if (centerObj.isMine) return null;

        // Empress探索範囲：まず5x5で最大探索（isReverse=trueの可能性があるため）
        const int maxRange = 2;

        for (int dx = -maxRange; dx <= maxRange; dx++)
        {
            for (int dy = -maxRange; dy <= maxRange; dy++)
            {
                int tx = center.x + dx;
                int ty = center.y + dy;
                // 範囲外チェック
                if (tx < 0 || ty < 0 || tx >= BoardManager.BOARD_SIZE || ty >= BoardManager.BOARD_SIZE)
                    continue;
                var target = BoardManager.singleton.onlinePieces[tx, ty];
                if (target == null) continue;

                var targetObj = target.GetComponent<PieceObject>();
                // Empress かつ isMain == false の駒だけが対象
                if (targetObj.GetPieceType() == PieceType.Empress && !targetObj.isMine)
                {
                    int requiredRange = targetObj.isReverse ? 2 : 1;
                    // Empress の中心から見た中心駒との距離が範囲内ならOK（逆向きチェック）
                    if (Mathf.Abs(tx - center.x) <= requiredRange && Mathf.Abs(ty - center.y) <= requiredRange)
                    {
                        List<int> bb = new List<int>(){tx,ty};
                        return bb;
                    }
                }
            }
        }

        return null;
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
