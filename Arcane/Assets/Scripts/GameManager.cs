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
    PieceSpawner pieceSpawner;

    [SerializeField]
    GameObject playerObjectPrefab;

    // プレイヤー数のカウント
    private int playerCount = 0;
    // プレイヤーのデータ
    [Networked,Capacity(2)]
    NetworkArray<NetworkObject> playerObjects => default;
    private bool[] isReady = new bool[2]{false,false};

    public PieceType? selectedPiece{ get; set; }

    private bool isHandPiece = false;

    private GameObject selectedPieceObject;

    private List<GameObject> localPieces = new List<GameObject>();

    private void Awake(){
        singleton = this;
    }

    public void PlayerJoined(PlayerRef player)
    {
        if(HasStateAuthority){
            PlayerJoined_Rpc(player);
        }
    }
    [Rpc(RpcSources.StateAuthority,RpcTargets.All)]
    public void PlayerJoined_Rpc(PlayerRef player){
        // NOTE : PlayerJoinedがなぜか全クライアントで呼ばれないため、一時的にRPCを使って実現
        // TODO : PlayerJoinedが全クライアントで呼ばれるようにするための施策が思いつき次第修正
        if(Runner.LocalPlayer == player){
            Runner.Spawn(playerObjectPrefab,Vector3.zero,Quaternion.identity,player);
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
            GameStart_Rpc();
        }
    }
    
    [Rpc(RpcSources.StateAuthority,RpcTargets.All)]
    public void GameStart_Rpc(){
        StartCoroutine(DelayGameStart());
    }
    IEnumerator DelayGameStart(){
        yield return new WaitForSeconds(3);
        PlayerObject po = playerObjects[HasStateAuthority?0:1].GetComponent<PlayerObject>();
        po.SetDeck();
        for(int i = 0; i < 5; i++){
            po.DrawDeck();
        }
    }

    void Start()
    {
        boardManager.SetBoard();
    }

    void Update(){
        if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHithit = new RaycastHit();
            if(Physics.Raycast(ray,out raycastHithit)){
                GameObject hitObject = raycastHithit.collider.gameObject;
                if(hitObject.tag == "Board" && selectedPiece != null){
                    BoardBlock bb = hitObject.GetComponent<BoardBlock>();
                    if(bb.y <= 2){
                        SetPiece(selectedPiece ?? PieceType.Fool,bb.x,bb.y); // TODO nullの対処
                        PlayerObject po = playerObjects[HasStateAuthority?0:1].GetComponent<PlayerObject>();
                        if(isHandPiece){
                            po.RemoveHand(selectedPiece ?? PieceType.Fool); // TODO nullの対処
                        }else{
                            localPieces.Remove(selectedPieceObject);
                            Destroy(selectedPieceObject);
                        }
                        selectedPiece = null;
                    }
                }
                else if(hitObject.tag == "Piece"){
                    selectedPiece = hitObject.GetComponent<PieceObject>().GetPieceType();
                    isHandPiece = false;
                    selectedPieceObject = hitObject;
                }
            }
        }

    }
    public void SetPiece(PieceType pieceType,int posX,int posY){
        var piece = Instantiate(pieceSpawner.GetPiecePrefab(pieceType));
        piece.GetComponent<PieceObject>().RenderName();
        piece.GetComponent<PieceObject>().SetLocalPosition(posX, posY);
        SetPieceOnBoard(piece,posX,posY);
        localPieces.Add(piece);
    }
    public void SetPieceOnBoard(GameObject piece,int posX,int posY){
        boardManager.SetPiece(piece,posX,posY);
    }

    public void SetSelectedPieceFromHand(PieceType pieceType){
        selectedPiece = pieceType;
        isHandPiece = true;
        selectedPieceObject = null;
    }

    public void SwitchIsReady(){
        SwitchIsReady_Rpc(HasStateAuthority?0:1);
    }

    [Rpc(RpcSources.All,RpcTargets.All)]
    public void SwitchIsReady_Rpc(int index){
        isReady[index] = true;
        DoneReady();
    }
    public void DoneReady(){
        for(int i = 0; i < 2; i++){
            if(!isReady[i]){
                print($"{i} is not ready");
                return;
            }
        }
        print("call");
        foreach(GameObject piece in localPieces){
            PieceObject po = piece.GetComponent<PieceObject>();
            NetworkObject netWorkPiece = Runner.Spawn(pieceSpawner.GetPiecePrefab(piece.GetComponent<PieceObject>().GetPieceType()));
            netWorkPiece.gameObject.GetComponent<PieceObject>().SetPosition(po.x,po.y);
            Destroy(piece);
        }
    }
}
