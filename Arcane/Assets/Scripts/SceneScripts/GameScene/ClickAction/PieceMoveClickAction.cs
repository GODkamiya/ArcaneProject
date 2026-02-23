using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PieceMoveClickAction : IClickAction
{
    private GameObject latestPiece;
    private PieceMovement latestMove;
    private GameConfig _config;

    public PieceMoveClickAction(GameConfig config)
    {
        _config = config;
    }
    public void OnClickBoard(BoardBlock bb)
    {
        if (latestPiece == null) return;
        if (!latestMove.range[bb.x, bb.y]) return;
        var clickedPiece = BoardManager.singleton.onlinePieces[bb.x, bb.y];

        // 攻撃判定・特殊条件
        if (clickedPiece != null)
        {
            var targetObj = clickedPiece.GetComponent<PieceObject>();

            // 皇帝の特殊ルール
            if (targetObj.GetPieceType() == PieceType.Emperor)
            {
                bool attackerRev = latestPiece.GetComponent<PieceObject>().GetIsReverse();
                bool targetRev = targetObj.GetIsReverse();
                if (attackerRev != targetRev) return;
            }

            // 審判以外の駒が、自分の駒を攻撃しようとしたらリターン
            bool isJudgementReverse = latestPiece.GetComponent<PieceObject>().GetPieceType() == PieceType.Judgement && latestPiece.GetComponent<PieceObject>().GetIsReverse();
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
        latestPiece.GetComponent<PieceObject>().ChangeColor(latestPiece.GetComponent<PieceObject>().GetIsKing() ? Color.red : Color.white);
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
                if (tx < 0 || ty < 0 || tx >= _config.BoardSize || ty >= _config.BoardSize)
                    continue;
                var target = BoardManager.singleton.onlinePieces[tx, ty];
                if (target == null) continue;

                var targetObj = target.GetComponent<PieceObject>();
                // Empress かつ isMain == false の駒だけが対象
                if (targetObj.GetPieceType() == PieceType.Empress && !targetObj.isMine)
                {
                    int requiredRange = targetObj.GetIsReverse() ? 2 : 1;
                    // Empress の中心から見た中心駒との距離が範囲内ならOK（逆向きチェック）
                    if (Mathf.Abs(tx - center.x) <= requiredRange && Mathf.Abs(ty - center.y) <= requiredRange)
                    {
                        List<int> bb = new List<int>() { tx, ty };
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
        if (!piece.isMine)
        {
            BoardBlock bb = BoardManager.singleton.GetTile(piece.x, piece.y).GetComponent<BoardBlock>();
            OnClickBoard(bb);
            return;
        }
        ;
        if (!piece.GetCanMove()) return;
        int poX = pieceObject.GetComponent<PieceObject>().x;
        int poY = pieceObject.GetComponent<PieceObject>().y;

        bool CheckPiece(int x, int y, bool? reverse)
        {
            // 範囲がオーバーしている際はスキップ
            if (x < 0 || y < 0 || x >= _config.BoardSize || y >= _config.BoardSize) return false;
            var piece = BoardManager.singleton.onlinePieces[x, y];
            //近くに節制がいるとき
            if (piece == null) return false;
            if (piece.GetComponent<PieceObject>().isMine) return false;
            if (piece.GetComponent<PieceObject>().GetPieceType() != PieceType.Temperance) return false;
            //発動条件を満たしていれば移動・効果を封じる
            return reverse == null || piece.GetComponent<PieceObject>().GetIsReverse();
        }

        if (CheckPiece(poX, poY + 1, null) || CheckPiece(poX, poY - 1, true) || CheckPiece(poX - 1, poY, true) || CheckPiece(poX + 1, poY, true)) return;

        if (latestPiece != null)
        {
            if (latestPiece.GetComponent<PieceObject>().GetIsKing())
            {
                latestPiece.GetComponent<PieceObject>().ChangeColor(Color.red);
            }
            else
            {
                latestPiece.GetComponent<PieceObject>().ChangeColor(Color.white);
            }
        }
        latestPiece = pieceObject;
        pieceObject.GetComponent<PieceObject>().ChangeColor(Color.cyan);
        PieceMovement move = piece.GetPieceMovement();
        latestMove = move;
        BoardManager.singleton.ShowMovement(move);
        if (piece is ActivePieceObject activePiece && activePiece.CanSpellActiveEffectMaster() && activePiece.GetCanSpell())
        {
            UIManager.singleton.ShowAbilityButton(() => activePiece.ActiveEffect());
        }
        else
        {
            UIManager.singleton.HideAbilityButton();
        }
    }
}
