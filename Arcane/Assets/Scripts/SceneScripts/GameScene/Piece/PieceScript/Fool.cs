using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fool : PieceObject
{
    public override string GetName()
    {
        return "愚者";
    }

    public override PieceMovement GetPieceMovementOrigin(int baseX, int baseY)
    {
        PieceMovement pm = new PieceMovement();

        List<(int, int)> directions = new List<(int, int)>();

        if (!isReverse)
        {
            directions.Add((0, 1)); // 前方のみ
        }
        else
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0) continue;
                    directions.Add((dx, dy)); // 8方向
                }
            }
        }
        foreach (var (dx, dy) in directions)
        {
            var target = FindJumpDestination(baseX + dx, baseY + dy, dx, dy);
            if (target != null)
            {
                pm.AddRange(target.Value.Item1, target.Value.Item2);
            }
        }
        return pm;
    }
    private (int, int)? FindJumpDestination(int x, int y, int dx, int dy)
    {
        if (!IsInsideBoard(x, y))
            return null;

        var piece = BoardManager.singleton.onlinePieces[x, y];
        if (piece == null)
        {
            // 駒がいなければそこが最終目的地
            return (x, y);
        }

        // 駒がいたらさらに同じ方向にジャンプ
        return FindJumpDestination(x + dx, y + dy, dx, dy);
    }
    private bool IsInsideBoard(int x, int y)
    {
        return x >= 0 && x < BoardManager.BOARD_SIZE && y >= 0 && y < BoardManager.BOARD_SIZE;
    }
    public override PieceType GetPieceType()
    {
        return PieceType.Fool;
    }

    public override string GetReverseEffectDescription()
    {
        return "";
    }

    public override string GetUprightEffectDescription()
    {
        return "";
    }
}
