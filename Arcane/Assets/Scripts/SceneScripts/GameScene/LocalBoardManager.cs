using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

/// <summary>
/// 相手に共有されていないローカルのボードの情報を扱うクラス
/// </summary>
public class LocalBoardManager
{
    public GameObject[,] pieces = new GameObject[BoardManager.BOARD_SIZE, BoardManager.BOARD_SIZE];

    private GameObject kingPieceObject = null;

    public bool HasPiece(int x, int y)
    {
        return pieces[x, y];
    }

    public GameObject SetPiece(PieceType pieceType, int x, int y)
    {
        var piece = GameObject.Instantiate(PieceSpawner.singleton.GetPiecePrefab(pieceType));
        piece.GetComponent<PieceObject>().RenderName();
        piece.GetComponent<PieceObject>().SetLocalPosition(x, y);
        pieces[x, y] = piece;

        // ボード上に実際に置く
        BoardManager.singleton.SetPieceOnBoard(piece, x, y, false);
        return piece;
    }

    public void RemovePiece(GameObject selectedPieceObject)
    {
        PieceObject pieceObject = selectedPieceObject.GetComponent<PieceObject>();
        pieces[pieceObject.x, pieceObject.y] = null;
        GameObject.Destroy(selectedPieceObject);
    }

    public int GetPieceCount()
    {
        return GetPieceList().Count;
    }

    public List<GameObject> GetPieceList()
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < BoardManager.BOARD_SIZE; i++)
        {
            for (int j = 0; j < BoardManager.BOARD_SIZE; j++)
            {
                if (pieces[i, j] != null) list.Add(pieces[i, j]);
            }
        }
        return list;
    }

    public void SetKing()
    {
        kingPieceObject.GetComponent<PieceObject>().SetIsKing_Local(true);
    }

    public void SelectKing(GameObject pieceObject)
    {
        if (kingPieceObject != null)
        {
            kingPieceObject.GetComponent<PieceObject>().ChangeColor(Color.white);
        }
        kingPieceObject = pieceObject;
        pieceObject.GetComponent<PieceObject>().ChangeColor(Color.red);
    }

    public bool HasKing()
    {
        return kingPieceObject != null;
    }
}
