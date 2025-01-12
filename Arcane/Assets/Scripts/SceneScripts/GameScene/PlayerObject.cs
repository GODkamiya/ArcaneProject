using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using TMPro;
using UnityEngine;

public class PlayerObject : NetworkBehaviour
{
    [Networked, Capacity(22)]
    NetworkArray<PieceType> deck => default;
    [Networked, OnChangedRender(nameof(RenderPanel))]
    int drawCount { get; set; } = 0;
    [Networked, Capacity(22), OnChangedRender(nameof(RenderHand))]
    NetworkLinkedList<PieceType> hand => default;
    [SerializeField]
    GameObject pieceUIPrefab;
    // このオブジェクトが対応しているパネルが入る。
    // 自分から見て、どちらかが入り、同期されない。
    private PlayerPanel responsePanel;
    private GameObject handPanel;
    public PieceType? kingPieceType { get; set; }
    public GameObject? kingPieceObject { get; set; }
    // プレイヤーが生成され次第、プレイヤーを登録する
    public override void Spawned()
    {
        GameObject allyPanel = GameObject.Find("Canvas/AllyPanel");
        GameObject enemyPanel = GameObject.Find("Canvas/EnemyPanel");

        responsePanel = HasStateAuthority ? allyPanel.GetComponent<PlayerPanel>() : enemyPanel.GetComponent<PlayerPanel>();
        if (HasStateAuthority)
        {
            handPanel = GameObject.Find("Canvas/HandPanel");
            GameManager.singleton.AddPlayerObject_Rpc(GetComponent<NetworkObject>());
        }
    }


    /// <summary>
    /// 新たにデッキを生成し、シャッフルする
    /// </summary>
    public void SetDeck()
    {
        int count = 0;
        PieceType[] pieceTypes = new PieceType[22];
        foreach (PieceType pieceType in Enum.GetValues(typeof(PieceType)))
        {
            pieceTypes[count] = pieceType;
            count++;
        }
        //シャッフル処理
        // TODO worldは初期手札に入らない
        bool containsWorld;
        do
        {
            // デッキをランダムにシャッフル
            PieceType[] shuffle = pieceTypes.OrderBy(i => Guid.NewGuid()).ToArray();
            count = 0;

            // シャッフルしたデッキをセット
            foreach (PieceType pieceType in shuffle)
            {
                deck.Set(count, pieceType);
                count++;
            }

            // デッキの上位5枚に World が含まれているか確認
            containsWorld = false;
            for (int i = 0; i < 5; i++)
            {
                if (deck.Get(i) == PieceType.World)
                {
                    containsWorld = true;
                    break;
                }
            }
        } while (containsWorld); // World がある場合、再シャッフル
    }

    /// <summary>
    /// カードを1枚ドローする
    /// </summary>
    public void DrawDeck()
    {
        PieceType result = deck.Get(drawCount);
        drawCount++;
        hand.Add(result);
        Debug.Log(result);
    }

    /// <summary>
    /// 手持ちのコマをDebugLogに表示するデバッグ用
    /// </summary>
    public void PrintHand()
    {
        foreach (PieceType pieceType in hand)
        {
            Debug.Log(pieceType);
        }
    }

    public void RenderPanel()
    {
        responsePanel.SetText(deckAmount: 22 - drawCount, handAmount: hand.Count);
    }

    public void RenderHand()
    {
        if (!HasStateAuthority)
        {
            return;
        }
        foreach (Transform child in handPanel.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (PieceType pieceType in hand)
        {
            GameObject pieceUI = Instantiate(pieceUIPrefab);
            pieceUI.GetComponent<PieceUIScript>().pieceType = pieceType;
            pieceUI.GetComponentInChildren<TextMeshProUGUI>().text = pieceType.ToString();
            pieceUI.transform.SetParent(handPanel.transform, false);
        }
    }

    public void RemoveHand(PieceType pieceType)
    {
        hand.Remove(pieceType);
    }
    public void AddHand(PieceType pieceType){
        hand.Add(pieceType);
    }
    public bool HasOneCard(){
        return hand.Count > 0;
    }
    public bool HasSelectedKing(){
        return kingPieceType != null;
    }
    public void SetKingPieceType(GameObject pieceObject){
        if(kingPieceObject != null){
            kingPieceObject.GetComponent<Renderer>().material.color = Color.white;
        }
        kingPieceType = pieceObject.GetComponent<PieceObject>().GetPieceType();
        kingPieceObject = pieceObject;
        pieceObject.GetComponent<Renderer>().material.color = Color.red;
    }
}
