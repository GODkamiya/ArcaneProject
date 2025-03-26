using Fusion;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class HangedMan : PieceObject, IOnReverse
{
    private PieceType? pretender;

    public override void Spawned()
    {
    }

    public override string GetName()
    {
        return PieceTypeExtension.GetNameFromPieceType(pretender ?? PieceType.HangedMan);
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

    public void OnReverse()
    {
        // TODO : ここDeathと共通化できるならしたい。要は消える処理みたいな感じ
        isLiving = false;
        // そこにいるのがまだ自分の場合のみ、ボード上から削除 (移動で踏みつぶされている場合は消さない)
        if (BoardManager.singleton.onlinePieces[x, y] == gameObject) BoardManager.singleton.RemovePieceOnBoard(x, y);
        // 消すことによって同期処理が意図せず終了することがあるため、表示上で場外に飛ばす
        gameObject.transform.position = new Vector3(-100, 100, -100);

        // 新しいコマを配置する
        if(HasStateAuthority){
            LocalBoardManager localBoard = new LocalBoardManager();
            GameObject newPiece = localBoard.SetPiece(pretender ?? PieceType.HangedMan,x,y);
            if(isKing)localBoard.SelectKing(newPiece);
            BoardManager.singleton.AsyncPiece(GameManager.singleton.Runner,true,localBoard);
        }
    }

    public override string GetUprightEffectDescription()
    {
        return "このコマは召喚する際に、「吊るされた男」以外のコマ１体に偽装する。動きや効果は変わらない。";
    }

    public override string GetReverseEffectDescription()
    {
        return "偽装しているコマに変身する。";
    }
}
