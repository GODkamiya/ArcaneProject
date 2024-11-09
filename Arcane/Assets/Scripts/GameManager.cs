using System.Collections;
using System.Collections.Generic;
using Fusion;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : NetworkBehaviour,IPlayerJoined
{
    public static GameManager singleton;

    [SerializeField]
    BoardManager boardManager;
    [SerializeField]
    PieceSpawner pieceSpawner;
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
        if(HasStateAuthority){
            Runner.Spawn(playerObjectPrefab,Vector3.zero,Quaternion.identity,player);
        }
    }
    /// <summary>
    /// PlayerObjectを新たに登録する
    /// </summary>
    /// <param name="playerObject"></param>
    [Rpc(RpcSources.All,RpcTargets.StateAuthority)]
    public void AddPlayerObject_Rpc(NetworkObject playerObject){
        print("call AddPlayerObject");
        playerObjects.Set(playerCount,playerObject);
        playerCount++;
        if(playerCount == 2){
            GameStart_Rpc();
        }
    }
    
    [Rpc(RpcSources.StateAuthority,RpcTargets.All)]
    public void GameStart_Rpc(){
        StartCoroutine(DelayGameStart());
    }
    IEnumerator DelayGameStart(){
        yield return new WaitForSeconds(3);
        Debug.Log("GameStart");
        PlayerObject po = playerObjects[HasStateAuthority?0:1].GetComponent<PlayerObject>();
        po.SetDeck();
        for(int i = 0; i < 5; i++){
            po.DrawDeck();
        }
        
    }

    void Start()
    {
        boardManager.SetBoard();
        var piece = Instantiate(pieceSpawner.GetPiecePrefab(PieceType.Fool));
        piece.GetComponent<PieceObject>().RenderName();
        boardManager.SetPiece(piece,0,0);
    }
}
