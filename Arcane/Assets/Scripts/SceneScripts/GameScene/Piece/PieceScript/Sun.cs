using Fusion;
using UnityEngine;

public class Sun : ActivePieceObject
{
    private bool isTransformed = false;

    private GameConfig _config;
    public Sun(GameConfig config)
    {
        _config = config;
    }

    public override void ActiveEffect()
    {
        canActive = false;
        RenderName_RPC();
        SetReverse_RPC(false);
        GameManager.singleton.phaseMachine.TransitionTo(new ActionPhase());
    }
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RenderName_RPC()
    {
        isTransformed = !isTransformed; // 変わったことを記録
        RenderName();
    }

    public override bool CanSpellActiveEffect()
    {
        return canActive;
    }

    public override PieceMovement GetPieceMovementOrigin(int baseX, int baseY)
    {
        PieceMovement pm = new PieceMovement(_config);

        if (GetIsReverse())
        {
            if (isTransformed)
            {
                // 角の動き（斜め方向に無制限に移動可能）
                int[] directions = { -1, 1 };
                foreach (int dx in directions)
                {
                    foreach (int dy in directions)
                    {
                        int x = baseX, y = baseY;
                        while (true)
                        {
                            x += dx;
                            y += dy;

                            if (x < 0 || x >= _config.BoardSize || y < 0 || y >= _config.BoardSize) break;

                            pm.AddRange(x, y);
                        }
                    }
                }
            }
            else
            {
                // 飛車の動き（縦横無制限に移動可能）
                int[] directions = { -1, 1 };

                // 横方向（左右）
                foreach (int dx in directions)
                {
                    int x = baseX;
                    while (true)
                    {
                        x += dx;
                        // 盤の範囲外なら終了
                        if (x < 0 || x >= _config.BoardSize) break;
                        pm.AddRange(x, baseY);
                    }
                }

                // 縦方向（上下）
                foreach (int dy in directions)
                {
                    int y = baseY;
                    while (true)
                    {
                        y += dy;
                        // 盤の範囲外なら終了
                        if (y < 0 || y >= _config.BoardSize) break;
                        pm.AddRange(baseX, y);
                    }
                }
            }
        }
        else
        {
            // 通常時の動き（周囲1マス）
            for (int addX = -1; addX <= 1; addX++)
            {
                for (int addY = -1; addY <= 1; addY++)
                {
                    if (addX == 0 && addY == 0) continue;

                    int newX = baseX + addX;
                    int newY = baseY + addY;

                    // 盤の範囲内なら追加
                    if (newX >= 0 && newX < _config.BoardSize && newY >= 0 && newY < _config.BoardSize)
                    {
                        pm.AddRange(newX, newY);
                    }
                }
            }
        }

        return pm;
    }

    public override PieceType GetPieceType()
    {
        return isTransformed ? PieceType.Moon : PieceType.Sun;
    }
}
