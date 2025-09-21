using System;
using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;

public abstract class PieceObject : NetworkBehaviour
{
    // TODO : x,yはControllerに畳み込まれるべき
    public int x, y;

    /// <summary>
    /// このコマが自分のものであるかどうか
    /// </summary>
    public bool isMine => HasStateAuthority;

    // このコマに関する情報が変更した際に発火されるイベント
    public Action onChangeInformation;

    List<AddPieceMovement> addPieceMovementList = new List<AddPieceMovement>();

    /// <summary>
    /// コマの状態を管理するコントローラー
    /// </summary>
    private PieceController controller { get; set; }

    void Awake()
    {
        RenderName();
        controller = new PieceController();

    }

    public void SetLocalPosition(int newX, int newY)
    {
        x = newX;
        y = newY;
    }

    /// <summary>
    /// オンライン上で共有するコマの設置関数
    /// </summary>
    /// <param name="newX"></param>
    /// <param name="newY"></param>
    /// <param name="isAttack">これがfalseの場合、コマを取れない。WheelOfFortune用の引数</param>
    public void SetPosition(int newX, int newY, bool isAttack, bool isSummon)
    {
        if (isSummon) GameManager.singleton.SendLog(new SummonLog(GameManager.singleton.GetIs1P(), GetName()));
        SetPosition_RPC(newX, newY, isAttack, isSummon);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void SetPosition_RPC(int newX, int newY, NetworkBool isAttack, NetworkBool isSummon)
    {
        // 移動先を計算
        if (!HasStateAuthority)
        {
            newX = 9 - newX;
            newY = 9 - newY;
            gameObject.GetComponent<Renderer>().material.color = Color.gray;
            if (GetPieceType() == PieceType.Hermit)
            {
                Hermit hermitData = gameObject.GetComponent<Hermit>();
                hermitData.ToggleTransparent_RPC();
                if (hermitData.isTransparent)
                {
                    gameObject.GetComponent<Renderer>().enabled = false;
                    gameObject.GetComponentInChildren<TextMeshPro>().text = "";
                }
                else
                {
                    gameObject.GetComponent<Renderer>().enabled = true;
                    gameObject.GetComponentInChildren<TextMeshPro>().text = hermitData.GetName();
                }
            }
        }

        // 敵の判定
        PieceObject enemy = null;
        if (BoardManager.singleton.onlinePieces[newX, newY] != null && isAttack)
        {
            enemy = BoardManager.singleton.onlinePieces[newX, newY].GetComponent<PieceObject>();
        }

        // 先に移動を完了させる
        BoardManager.singleton.RemovePieceOnBoard(x, y);
        BoardManager.singleton.SetPieceOnBoard(gameObject, newX, newY, true);
        x = newX;
        y = newY;

        // 最後に敵との攻撃処理を実行する
        if (enemy != null)
        {
            if (isMine) GameManager.singleton.SendLog(new AttackLog(GameManager.singleton.GetIs1P(), GetName(), enemy.GetName(), !enemy.isMine));
            enemy.Death();
            if (this is IOnAttackEvent)
            {
                ((IOnAttackEvent)this).OnAttack(newX, newY, enemy);
            }
            SetReverse_RPC(true);
        }
        else
        {
            if (!isSummon && isMine) GameManager.singleton.SendLog(new MoveLog(GameManager.singleton.GetIs1P(), GetName()));
        }
    }
    public string GetName() => PieceTypeExtension.GetNameFromPieceType(GetPieceType());
    public string GetUprightEffectDescription() => PieceTypeExtension.GetUprightEffectDescriptionFromPieceType(GetPieceType());

    public string GetReverseEffectDescription() => PieceTypeExtension.GetReverseEffectDescriptionFromPieceType(GetPieceType());

    public int[,] GetMovementDefinitions() => PieceTypeExtension.GetMovementDefinitionsFromPieceType(GetPieceType());

    public int[,] GetReverseMovementDefinitions() => PieceTypeExtension.GetReverseMovementDefinitionsFromPieceType(GetPieceType());

    public void RenderName()
    {
        GetComponentInChildren<TextMeshPro>().text = GetName();
    }
    public abstract PieceType GetPieceType();

    public abstract PieceMovement GetPieceMovementOrigin(int baseX, int baseY);


    public PieceMovement GetPieceMovement()
    {
        return GetPieceMovement(x, y);
    }
    public PieceMovement GetPieceMovement(int baseX, int baseY)
    {
        PieceMovement pieceMove = GetPieceMovementOrigin(baseX, baseY);
        foreach (AddPieceMovement adder in addPieceMovementList)
        {
            pieceMove = adder.Add(baseX, baseY, pieceMove);
        }
        return pieceMove;
    }
    public void Death()
    {

        // そこにいるのがまだ自分の場合のみ、ボード上から削除 (移動で踏みつぶされている場合は消さない)
        if (BoardManager.singleton.onlinePieces[x, y] == gameObject) BoardManager.singleton.RemovePieceOnBoard(x, y);

        // 消すことによって同期処理が意図せず終了することがあるため、表示上で場外に飛ばす
        gameObject.transform.position = new Vector3(-100, 100, -100);


        // 王の場合、ゲーム終了へ
        if (GetIsKing())
        {
            GameManager.singleton.phaseMachine.TransitionTo(new GameEndPhase(GameManager.singleton.HasStateAuthority != HasStateAuthority));
        }

        if (GetPieceType() == PieceType.Tower)
        {
            for (int addX = -2; addX <= 2; addX++)
            {
                for (int addY = -2; addY <= 2; addY++)
                {
                    if (x + addX > 9 || y + addY > 9 || x + addX < 0 || y + addY < 0) continue;
                    BoardManager.singleton.onlinePieces[x + addX, y + addY]?.GetComponent<PieceObject>().Death();
                }
            }
        }
    }

    public void AddAddPieceMovement(AddPieceMovement adder)
    {
        addPieceMovementList.Add(adder);
    }
    public void RemoveAddPieceMovement(AddPieceMovement adder)
    {
        addPieceMovementList.Remove(adder);
    }

    /// <summary>
    /// 逆位置にするかどうかを設定するRPC
    /// </summary>
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void SetReverse_RPC(NetworkBool isReverse)
    {
        controller.SetReverse(isReverse);
        onChangeInformation?.Invoke();
        if (isReverse && this is IOnReverse)
        {
            ((IOnReverse)this).OnReverse();
        }
    }

    /// <summary>
    /// 移動可能かどうか
    /// </summary>
    public bool GetCanMove() => controller.GetCanMove;

    /// <summary>
    /// コマが技を使用可能かどうか
    /// </summary>
    public bool GetCanSpell() => controller.GetCanSpell;

    /// <summary>
    /// コマが王かどうかを取得する
    /// </summary>
    public bool GetIsKing() => controller.GetIsKing;

    /// <summary>
    /// コマが逆位置かどうかを取得する
    /// </summary>
    public bool GetIsReverse() => controller.GetIsReverse;

    /// <summary>
    /// コマを召喚酔い状態にするかどうかを設定する
    /// </summary>
    public void SetSickness(bool isSickness) => controller.SetSickness(isSickness);

    /// <summary>
    /// コマが王かどうかを同期して設定する
    /// </summary>
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void SetIsKing_RPC(NetworkBool isKing) => SetIsKing_Local(isKing);

    /// <summary>
    /// コマが王かどうかをローカルで設定する
    /// </summary>
    public void SetIsKing_Local(bool isKing)
    {
        controller.SetKing(isKing);
        if (GetIsKing() && HasStateAuthority)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
