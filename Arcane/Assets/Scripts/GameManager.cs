using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Fusion;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using VContainer;

public class GameManager : NetworkBehaviour, IPlayerJoined
{
    public static GameManager singleton;

    [SerializeField]
    BoardManager boardManager;

    [SerializeField]
    GameObject playerObjectPrefab;

    [SerializeField]
    GameObject commandPallet;


    // プレイヤー数のカウント
    private int playerCount = 0;
    // プレイヤーのデータ
    [Networked, Capacity(2)]
    NetworkArray<NetworkObject> playerObjects => default;
    private bool[] isReady = new bool[2] { false, false };

    private bool is1pTurn = true;

    public bool GetIs1P() => HasStateAuthority;
    public PlayerObject GetLocalPlayerObject() => playerObjects[HasStateAuthority ? 0 : 1].GetComponent<PlayerObject>();
    public PlayerObject GetEnemyPlayerObject() => playerObjects[HasStateAuthority ? 1 : 0].GetComponent<PlayerObject>();
    public PhaseMachine phaseMachine = new PhaseMachine();
    public TurnMachine turnMachine = new TurnMachine();

    public List<TurnEndEvent> turnEndEvents = new List<TurnEndEvent>();

    private IObjectResolver _container;

    [Inject]
    public void Construct(IObjectResolver container)
    {
        _container = container;
    }

    private void Awake()
    {
        singleton = this;
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.C))
        {
            commandPallet.SetActive(true);
        }
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
            var obj = Runner.Spawn(playerObjectPrefab, Vector3.zero, Quaternion.identity, player);
            var playerObject = obj.GetComponent<PlayerObject>();
            _container.Inject(playerObject);
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
        turnMachine.Initialize(new PreparationTurn());
    }

    void Start()
    {
        boardManager.SetBoard();
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
        boardManager.AsyncPiece(Runner, true, ((PreparationTurn)turnMachine.GetCurrentTurn()).GetLocalBoardManager());
        PhasePanel.singleton.Initialize(is1pTurn == HasStateAuthority);
        TurnStart();
    }
    public void TurnStart()
    {
        // フェーズUIの切り替え
        PhasePanel.singleton.SwitchPlayer();

        // ターン開始時の処理をコールする
        foreach (GameObject piece in boardManager.GetAllPieces())
        {
            PieceObject data = piece.GetComponent<PieceObject>();
            if (data is IOnTurnStart)
            {
                ((IOnTurnStart)data).OnTurnStart();
            }
        }

        if (is1pTurn == HasStateAuthority)
        {
            if (!GetLocalPlayerObject().HasRestDeck() && !GetLocalPlayerObject().HasOneCard())
            {
                phaseMachine.TransitionTo(new ActionPhase());
            }
            else
            {
                DrawOrSummonPhase();
            }
            foreach (GameObject piece in BoardManager.singleton.onlinePieces)
            {
                if (piece != null && piece.GetComponent<PieceObject>() is ActivePieceObject activePiece)
                {
                    activePiece.canActive = true;
                }
            }
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
        Log_RPC(SerializeLog(new DrawLog(GetIs1P())));
        ChangeActionPhaseUI_RPC();
        phaseMachine.TransitionTo(new ActionPhase());
    }
    public void SummonPhase()
    {
        phaseMachine.TransitionTo(new SummonPhase());
    }
    public void TurnEnd()
    {
        phaseMachine.TransitionTo(new WaitPhase());
        TurnEnd_RPC();
    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void TurnEnd_RPC()
    {
        turnEndEvents.ForEach(turnEndEvent => turnEndEvent.Do());
        is1pTurn = !is1pTurn;
        TurnStart();
    }
    public void ShutDown()
    {
        SceneManager.LoadScene("Matching");
        Runner.Shutdown();
    }

    // TODO これより下にあるログ関連の機能は、絶対にGameManagerにあるべきではない。

    /// <summary>
    /// ログを送信する
    /// </summary>
    /// <param name="log"></param>
    public void SendLog(LogBase log)
    {
        //Log_RPC(SerializeLog(log));
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void Log_RPC(byte[] serializedLog)
    {
        // var log = DeserializeLog(serializedLog);
        // Debug.Log(log.GetLogMessage());
        // InformationPanel.singleton.GetLogPanel().AddLog(log);
    }

    public byte[] SerializeLog(LogBase log)
    {
        using (var ms = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, log);
            return ms.ToArray();
        }
    }

    private LogBase DeserializeLog(byte[] data)
    {
        using (var ms = new MemoryStream(data))
        {
            var formatter = new BinaryFormatter();
            return (LogBase)formatter.Deserialize(ms);
        }
    }

    // TODO アクションに変更させるだけのRPC、絶対にここにあるべきではない。
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void ChangeActionPhaseUI_RPC()
    {
        PhasePanel.singleton.ChangePhase("アクション");
    }
}
