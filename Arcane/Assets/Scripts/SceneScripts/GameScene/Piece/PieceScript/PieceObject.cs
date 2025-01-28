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
    public void SetPosition(int newX, int newY,bool isAttack)
    {
        SetPosition_RPC(newX, newY,isAttack);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void SetPosition_RPC(int newX, int newY,NetworkBool isAttack)
    {
        // 以前いた位置のコマ情報を削除
        BoardManager.singleton.RemovePieceOnBoard(x, y);
        if (!HasStateAuthority)
        {
            newX = 9 - newX;
            newY = 9 - newY;
            gameObject.GetComponent<Renderer>().material.color = Color.gray;
            if(GetPieceType() == PieceType.Hermit){
                gameObject.GetComponent<Renderer>().enabled = false;
                gameObject.GetComponentInChildren<TextMeshPro>().text = "";
            }
        }
        x = newX;
        y = newY;
        if (BoardManager.singleton.onlinePieces[x, y] != null && isAttack)
        {
            PieceObject piece = BoardManager.singleton.onlinePieces[x, y].GetComponent<PieceObject>();
            piece.Death();
        }
        BoardManager.singleton.SetPieceOnBoard(gameObject, newX, newY, true);
    }
    public abstract String GetName();

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
        if(isKing && HasStateAuthority)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }

    }
    public abstract PieceMovement GetPieceMovementOrigin();

    public PieceMovement GetPieceMovement(){
        PieceMovement pieceMove = GetPieceMovementOrigin();
        foreach(AddPieceMovement adder in addPieceMovementList){
            pieceMove = adder.Add(x,y,pieceMove);
        }
        return pieceMove;
    }
    public void Death()
    {
        BoardManager.singleton.RemovePieceOnBoard(x, y);
        Destroy(gameObject);
        if (isKing)
        {
            GameManager.singleton.phaseMachine.TransitionTo(new GameEndPhase(GameManager.singleton.HasStateAuthority != HasStateAuthority));

        }
    }

    public void AddAddPieceMovement(AddPieceMovement adder){
        addPieceMovementList.Add(adder);
    }
    public void RemoveAddPieceMovement(AddPieceMovement adder){
        addPieceMovementList.Remove(adder);
    }
}
