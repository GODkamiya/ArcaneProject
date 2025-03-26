using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Fusion;
using TMPro;
using UnityEngine;

public abstract class PieceObject : NetworkBehaviour
{
    public int x, y;

    public bool isKing = false;
    public bool isMine => HasStateAuthority;

    // 逆位置かどうか

    public bool isReverse = false;

    // 召喚酔いしているかどうか
    public bool isSickness = true;

    // 生きているか
    public bool isLiving = true;

    //攻撃できるか
    public bool isAttackable = true;

    //死なないかどうか
    public bool isImmortality = false;

    // 節制の効果をうけているかどうか
    public GameObject temperance = null;

    List<AddPieceMovement> addPieceMovementList = new List<AddPieceMovement>();

    public override void Spawned()
    {
        RenderName();
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
    public void SetPosition(int newX, int newY, bool isAttack)
    {
        SetPosition_RPC(newX, newY, isAttack);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void SetPosition_RPC(int newX, int newY, NetworkBool isAttack)
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
            enemy.Death();
            if (this is IOnAttackEvent)
            {
                ((IOnAttackEvent)this).OnAttack(newX, newY, enemy);
            }
            SetReverse(true);
        }
    }
    public abstract string GetName();
    public abstract string GetUprightEffectDescription();

    public abstract string GetReverseEffectDescription();

    public void RenderName()
    {
        GetComponentInChildren<TextMeshPro>().text = GetName();
    }
    public abstract PieceType GetPieceType();

    public void SetKing(bool value)
    {
        SetKing_RPC(value ? 1 : 0);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void SetKing_RPC(int value)
    {
        isKing = value == 1;
        if (isKing && HasStateAuthority)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }

    }
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
        if (isImmortality) return;
        isLiving = false;

        // そこにいるのがまだ自分の場合のみ、ボード上から削除 (移動で踏みつぶされている場合は消さない)
        if (BoardManager.singleton.onlinePieces[x, y] == gameObject) BoardManager.singleton.RemovePieceOnBoard(x, y);

        // 消すことによって同期処理が意図せず終了することがあるため、表示上で場外に飛ばす
        gameObject.transform.position = new Vector3(-100, 100, -100);

        // 節制の効果をうけている場合、元の節制を動けるようにする
        if(temperance != null)temperance.GetComponent<Temperance>().target = null;

        // 王の場合、ゲーム終了へ
        if (isKing)
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
    /// 指定した駒の指定位置（正・逆）を決める
    /// </summary>
    /// <param name="isReverse"></param>
    public void SetReverse(bool isReverse)
    {
        SetReverse_RPC(isReverse);
    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void SetReverse_RPC(bool isReverse)
    {
        this.isReverse = isReverse;
        if (isReverse && this is IOnReverse)
        {
            ((IOnReverse)this).OnReverse();
        }
    }
    /// <summary>
    /// 指定した駒の攻撃可否を決める
    /// </summary>
    /// <param name="isAttackable"></param>
    public void SetAttackable(bool isAttackable)
    {
        SetAttackable_RPC(isAttackable);
    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void SetAttackable_RPC(bool isAttackable)
    {
        this.isAttackable = isAttackable;
    }
    /// <summary>
    /// 指定した駒が死なないかどうか決める
    /// </summary>
    /// <param name="isImmortality"></param>
    public void SetImmortality(bool isImmortality)
    {
        SetImmortality_RPC(isImmortality);
    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void SetImmortality_RPC(bool isImmortality)
    {
        this.isImmortality= isImmortality;
    }

    public void SetTemperance(GameObject temperance)
    {
        this.temperance = temperance;
    }
}
