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

    public override void Spawned()
    {
        RenderName();
    }

    public void SetLocalPosition(int newX, int newY)
    {
        x = newX;
        y = newY;
    }
    public void SetPosition(int newX, int newY)
    {
        SetPosition_RPC(newX, newY);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void SetPosition_RPC(int newX, int newY)
    {
        if (!HasStateAuthority)
        {
            newX = 9 - newX;
            newY = 9 - newY;
        }
        x = newX;
        y = newY;

        if (BoardManager.singleton.onlinePieces[x, y] != null)
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
        if (HasStateAuthority)
        {
            if (isKing)
            {
                gameObject.GetComponent<Renderer>().material.color = Color.red;
            }

        }

    }
    public abstract PieceMovement GetPieceMovement();
    public void Death()
    {
        BoardManager.singleton.RemovePieceOnBoard(x, y);
        Destroy(gameObject);
        if (isKing)
        {
            GameManager.singleton.phaseMachine.TransitionTo(new GameEndPhase(GameManager.singleton.HasStateAuthority != HasStateAuthority));

        }
    }
}
