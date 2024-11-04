using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class GameManager : NetworkBehaviour,IPlayerJoined
{
    public static GameManager singleton;
    public static NetworkObject localPlayer;

    [SerializeField]
    BoardManager boardManager;
    [SerializeField]
    GameObject piecePrefab;
    [SerializeField]
    NetWorkManagerScript netWorkManager;

    [SerializeField]
    GameObject playerObjectPrefab;

    // プレイヤー数のカウント
    private int playerCount = 0;
    // プレイヤーのデータ
    [Networked,Capacity(2)]
    NetworkArray<NetworkObject> playerObjects => default;

    private void Awake(){
        singleton = this;
    }

    public void PlayerJoined(PlayerRef player)
    {
        if(player == Runner.LocalPlayer){
            var playerObject = Runner.Spawn(playerObjectPrefab,Vector3.zero,Quaternion.identity,player);
            localPlayer = playerObject;
        }
    }
    /// <summary>
    /// PlayerObjectを新たに登録する
    /// </summary>
    /// <param name="playerObject"></param>
    [Rpc(RpcSources.All,RpcTargets.StateAuthority)]
    public void AddPlayerObject_Rpc(NetworkObject playerObject){
        playerObjects.Set(playerCount,playerObject);
        playerCount++;
        if(playerCount == 2){
            Debug.Log("ゲームスタート");
        }
    }

    void Start()
    {
        boardManager.SetBoard();
        boardManager.SetPiece(Instantiate(piecePrefab),0,0);
    }


}
