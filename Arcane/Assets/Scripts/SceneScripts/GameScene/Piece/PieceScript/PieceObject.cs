using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;

public abstract class PieceObject : NetworkBehaviour
{
    public int x,y;

    public bool isKing = false;

    public override void Spawned()
    {
        RenderName();
    }

    public void SetLocalPosition(int newX,int newY){
        x = newX;
        y = newY;
    }
    public void SetPosition(int newX,int newY){
        SetPosition_RPC(newX,newY);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void SetPosition_RPC(int newX,int newY){
        if(!HasStateAuthority){
            newX = 9 - newX;
            newY = 9 - newY;
        }
        BoardManager.singleton.SetPieceOnBoard(gameObject, newX,newY);
    }
    public abstract String GetName();

    public void RenderName(){
        GetComponentInChildren<TextMeshPro>().text = GetName();
    }
    public abstract PieceType GetPieceType();

    public void SetKing(bool value){
        SetKing_RPC(value?1:0);
    }

    [Rpc(RpcSources.StateAuthority,RpcTargets.All)]
    public void SetKing_RPC(int value) {
        isKing = value == 1;
        if(isKing){
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
