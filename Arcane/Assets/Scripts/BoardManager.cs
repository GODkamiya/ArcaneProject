using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Fusion;
using Unity.VisualScripting;
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
    public const int BOARD_SIZE = 10;
    private GameObject[,] board = new GameObject[BOARD_SIZE, BOARD_SIZE];
    public GameObject[,] onlinePieces = new GameObject[BOARD_SIZE, BOARD_SIZE];

    private void Awake()
    {
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
                board[x, y] = tile;
            }
        }
    }
    /// <summary>
    /// 駒をボードに配置する
    /// </summary>
    /// <param name="piece">配置したい駒</param>
    /// <param name="x">駒の座標</param>
    /// <param name="y">駒の座標</param>
    public void SetPieceOnBoard(GameObject piece, int x, int y, bool isOnline)
    {
        piece.transform.position = new Vector3(x, 0.5f, y);
        if (isOnline) onlinePieces[x, y] = piece;
    }
    public void RemovePieceOnBoard(int x, int y)
    {
        onlinePieces[x, y] = null;
    }
    /// <summary>
    /// LocalBoardManagerを基に、相手にコマを共有する
    /// </summary>
    /// <param name="runner"></param>
    /// <param name="isFirstSummon"></param>
    /// <param name="localBoardManager"></param>
    public void AsyncPiece(NetworkRunner runner, bool isFirstSummon, LocalBoardManager localBoardManager)
    {
        foreach (GameObject piece in localBoardManager.GetPieceList())
        {
            PieceObject po = piece.GetComponent<PieceObject>();
            NetworkObject netWorkPiece = runner.Spawn(pieceSpawner.GetPiecePrefab(piece.GetComponent<PieceObject>().GetPieceType()));
            PieceObject networkPieceObject = netWorkPiece.gameObject.GetComponent<PieceObject>();
            networkPieceObject.SetPosition(po.x, po.y, true, true);
            networkPieceObject.SetIsKing_RPC(po.GetIsKing());
            networkPieceObject.SetReverse_RPC(po.isReverse);
            if (isFirstSummon)
            {
                networkPieceObject.SetSickness(false);
            }
            else
            {
                GameManager.singleton.turnEndEvents.Add(
                    new TurnEndEvent(1, () => networkPieceObject.SetSickness(false))
                );
            }
            if (po.GetPieceType() == PieceType.HangedMan)
            {
                HangedMan localHangedman = po.GetComponent<HangedMan>();
                HangedMan networkHangedman = networkPieceObject.GetComponent<HangedMan>();
                networkHangedman.AsyncSetPretender(localHangedman.GetPretender() ?? PieceType.HangedMan);
            }
            Destroy(piece);
        }
    }
    public void ShowMovement(PieceMovement pieceMovement)
    {
        ClearMovement();
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (pieceMovement.range[x, y])
                {
                    board[x, y].GetComponent<Renderer>().material.color = Color.green;
                }
            }
        }
    }
    public void ClearMovement()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                board[x, y].GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }

    public List<GameObject> GetAllPieces()
    {
        List<GameObject> result = new List<GameObject>();
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (onlinePieces[x, y] != null)
                {
                    result.Add(onlinePieces[x, y]);
                }
            }
        }
        return result;
    }
}
