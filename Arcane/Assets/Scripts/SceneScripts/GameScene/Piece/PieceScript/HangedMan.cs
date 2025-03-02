using Fusion;
using UnityEngine;

public class HangedMan : PieceObject
{
    private PieceType? pretender;

    public override void Spawned()
    {
    }

    public override string GetName()
    {
        return pretender == null ? "HangedMan" : pretender.ToString();
    }

    public override PieceMovement GetPieceMovementOrigin(int baseX, int baseY)
    {
        PieceMovement pm = new PieceMovement();
        for (int addX = -1; addX < 2; addX++)
        {
            for (int addY = -1; addY < 2; addY++)
            {
                if (addX == 0 && addY == 0) continue;
                pm.AddRange(baseX + addX, baseY + addY);
            }
        }
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.HangedMan;
    }

    public void SetPretender(PieceType predenter)
    {
        this.pretender = predenter;
        RenderName();
    }

    public PieceType? GetPretender()
    {
        return pretender;
    }

    public void AsyncSetPretender(PieceType pieceType)
    {
        AsyncSetPretender_RPC(pieceType);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void AsyncSetPretender_RPC(PieceType pieceType)
    {
        SetPretender(pieceType);
    }
}
