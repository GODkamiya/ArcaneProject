using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;

public abstract class PieceObject : NetworkBehaviour
{
    public int x,y;

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
        GameManager.singleton.SetPieceOnBoard(gameObject, newX,newY);
    }
    public abstract String GetName();

    public void RenderName(){
        GetComponentInChildren<TextMeshPro>().text = GetName();
    }
    public abstract PieceType GetPieceType();
}
