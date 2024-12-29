using System.Collections;
using System.Collections.Generic;
using Fusion;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : NetworkBehaviour, IPlayerJoined
{
    public static GameManager singleton;

    [SerializeField]
    BoardManager boardManager;


    [SerializeField]
    GameObject playerObjectPrefab;
    

    // プレイヤー数のカウント
    private int playerCount = 0;
    // プレイヤーのデータ
    [Networked, Capacity(2)]
    NetworkArray<NetworkObject> playerObjects => default;
    private bool[] isReady = new bool[2] { false, false };

    private bool is1pTurn = true;

    public PlayerObject GetLocalPlayerObject() => playerObjects[HasStateAuthority ? 0 : 1].GetComponent<PlayerObject>();
    public PlayerObject GetEnemyPlayerObject() => playerObjects[HasStateAuthority ? 1 : 0].GetComponent<PlayerObject>();
    public PhaseMachine phaseMachine = new PhaseMachine();
    private void Awake()
    {
        singleton = this;
    }

    public void PlayerJoined(PlayerRef player)
    {
        if (HasStateAuthority)
        {
            PlayerJoined_Rpc(player);
        }
    }
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void PlayerJoined_Rpc(PlayerRef player)
    {
        // NOTE : PlayerJoinedがなぜか全クライアントで呼ばれないため、一時的にRPCを使って実現
        // TODO : PlayerJoinedが全クライアントで呼ばれるようにするための施策が思いつき次第修正
        if (Runner.LocalPlayer == player)
        {
            Runner.Spawn(playerObjectPrefab, Vector3.zero, Quaternion.identity, player);
        }
    }

    /// <summary>
    /// PlayerObjectを新たに登録する
    /// </summary>
    /// <param name="playerObject"></param>
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void AddPlayerObject_Rpc(NetworkObject playerObject)
    {
        playerObjects.Set(playerCount, playerObject);
        playerCount++;
        if (playerCount == 2)
        {
            GameStart_Rpc();
        }
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void GameStart_Rpc()
    {
        PlayerObject po = playerObjects[HasStateAuthority ? 0 : 1].GetComponent<PlayerObject>();
        po.SetDeck();
        for (int i = 0; i < 5; i++)
        {
            po.DrawDeck();
        }
        phaseMachine.Initialize(new InitialSummonPhase());
    }

    void Start()
    {
        boardManager.SetBoard();
    }
    public void SwitchIsReady()
    {
        if (GetLocalPlayerObject().HasSelectedKing())
        {
            BoardManager.singleton.SetKing();
            SwitchIsReady_Rpc(HasStateAuthority ? 0 : 1);
        }
        else
        {
            PlayerClickHandler.singleton.SwitchIsSelectKing();
            phaseMachine.TransitionTo(new InitialSelectKingPhase());
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void SwitchIsReady_Rpc(int index)
    {
        isReady[index] = true;
        DoneReady();
    }
    public void DoneReady()
    {
        for (int i = 0; i < 2; i++)
        {
            if (!isReady[i])
            {
                print($"{i} is not ready");
                return;
            }
        }
        print("call");
        boardManager.AsyncPiece(Runner);
        TurnStart();
    }
    public void TurnStart()
    {
        if (is1pTurn == HasStateAuthority)
        {
            DrawOrSummonPhase();
        }
    }
    /// <summary>
    /// ドローか召喚かを問う時間
    /// </summary>
    public void DrawOrSummonPhase()
    {
        
        phaseMachine.TransitionTo(new DrawOrSummonPhase());
    }
    public void DrawPhase()
    {
        GetLocalPlayerObject().DrawDeck();
        
        PlayerClickHandler.singleton.isPieceMovementPhase = true;
        phaseMachine.TransitionTo(new ActionPhase());
    }
    public void SummonPhase()
    {

    }
    public void TurnEnd()
    {
        TurnEnd_RPC();
    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void TurnEnd_RPC()
    {
        is1pTurn = !is1pTurn;
        TurnStart();
    }
}
