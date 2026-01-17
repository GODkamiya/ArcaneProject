using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// コマの移動パネルの部分だけを専門に取り扱う
/// </summary>
public class DescriptionMovementPanelViewObject : MonoBehaviour
{
    /// <summary>
    /// 青いタイル1つのPrefab
    /// </summary>
    [SerializeField]
    private GameObject tilePrefab;

    /// <summary>
    /// コマの現在位置を示す赤いマーカーのPrefab
    /// </summary>
    [SerializeField]
    private GameObject locationPrefab;


    /// <summary>
    /// 左上を(0,0)、右下を(4,4)とした配列
    /// </summary>
    private GameObject[,] tiles = new GameObject[5, 5];

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject tile = Instantiate(tilePrefab, transform);
                tiles[i, j] = tile;
                if (i == 2 && j == 2)
                {
                    // 中央に赤いマーカーを置く
                    Instantiate(locationPrefab, tile.transform);
                }
            }
        }
        ResetTiles();
    }

    /// <summary>
    /// コマの移動可能範囲を表示する
    /// </summary>
    public void ShowMovableTiles(int[,] movementDefinitions)
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                tiles[i, j].GetComponent<Image>().color = GetColorFromMovement(movementDefinitions[i, j]);
            }
        }
    }

    private void ResetTiles()
    {
        ShowMovableTiles(new int[5, 5] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } });
    }

    private Color GetColorFromMovement(int moveId)
    {
        switch (moveId)
        {
            case 0:
                return new Color(0.5f, 0.5f, 0.5f, 1f);
            case 1:
                return new Color(1f, 1f, 1f, 1f);
            case 2:
                return new Color(0f, 1f, 0f, 1f);
            default:
                throw new InvalidEnumArgumentException();
        }
    }
}
