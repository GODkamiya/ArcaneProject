using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Fusion;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager singleton;
    [SerializeField]
    GameObject tilePrefab;
    [SerializeField]
    PieceSpawner pieceSpawner;
    /// <summary>
    /// ボードのサイズ
    /// </summary>
    const int BOARD_SIZE = 10;
    private List<GameObject> localPieces = new List<GameObject>();

    private void Awake(){
        singleton = this;
    }

    /// <summary>
    /// 盤を新たに生成する
    /// </summary>
    public void SetBoard()
    {
        for (int x = 0; x < BOARD_SIZE; x++)
        {
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                GameObject tile = Instantiate(tilePrefab);
                tile.GetComponent<BoardBlock>().x = x;
                tile.GetComponent<BoardBlock>().y = y;
                Vector3 pos = new Vector3(x, 0, y);
                tile.transform.position = pos;
            }
        }
    }
    /// <summary>
    /// 駒をボードに配置する
    /// </summary>
    /// <param name="piece">配置したい駒</param>
    /// <param name="x">駒の座標</param>
    /// <param name="y">駒の座標</param>
    public void SetPieceOnBoard(GameObject piece, int x, int y)
    {
        piece.transform.position = new Vector3(x, 0.5f, y);
    }
    public void SetPiece(PieceType pieceType, int posX, int posY)
    {
        var piece = Instantiate(pieceSpawner.GetPiecePrefab(pieceType));
        piece.GetComponent<PieceObject>().RenderName();
        piece.GetComponent<PieceObject>().SetLocalPosition(posX, posY);
        SetPieceOnBoard(piece, posX, posY);
        localPieces.Add(piece);
    }
    public void AsyncPiece(NetworkRunner runner)
    {
        foreach (GameObject piece in localPieces)
        {
            PieceObject po = piece.GetComponent<PieceObject>();
            NetworkObject netWorkPiece = runner.Spawn(pieceSpawner.GetPiecePrefab(piece.GetComponent<PieceObject>().GetPieceType()));
            netWorkPiece.gameObject.GetComponent<PieceObject>().SetPosition(po.x, po.y);
            Destroy(piece);
        }
    }
    public void RemoveLocalPiece(GameObject selectedPieceObject){
        localPieces.Remove(selectedPieceObject);
        Destroy(selectedPieceObject);
    }
}
