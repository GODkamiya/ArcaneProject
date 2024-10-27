using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    GameObject tilePrefab;
    /// <summary>
    /// ボードのサイズ
    /// </summary>
    const int BOARD_SIZE = 10;

    /// <summary>
    /// 盤を新たに生成する
    /// </summary>
    public void SetBoard()
    {
        for(int x = 0; x < BOARD_SIZE; x++)
        {
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                GameObject tile = Instantiate(tilePrefab);
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
    public void SetPiece(GameObject piece,int x,int y)
    {
        piece.transform.position = new Vector3(x, 0.5f, y);
    }
}
