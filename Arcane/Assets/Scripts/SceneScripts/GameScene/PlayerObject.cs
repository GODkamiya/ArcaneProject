using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using UnityEngine;

public class PlayerObject : NetworkBehaviour
{
    [Networked,Capacity(22)]
    NetworkArray<PieceType> deck => default;
    [Networked]
    int drawCount{ get; set; } = 0;
    [Networked,Capacity(22)]
    NetworkLinkedList<PieceType> hand => default;

    // プレイヤーが生成され次第、プレイヤーを登録する
    public override void Spawned(){
        if(HasStateAuthority){
            GameManager.singleton.AddPlayerObject_Rpc(GetComponent<NetworkObject>());
            Debug.Log("Yeeee");
        }
    }

    /// <summary>
    /// 新たにデッキを生成し、シャッフルする
    /// </summary>
    public void SetDeck(){
        int count = 0;
        PieceType[] pieceTypes = new PieceType[22];
        foreach(PieceType pieceType in Enum.GetValues(typeof(PieceType))){
            pieceTypes[count] = pieceType;
            count++;
        }
        //シャッフル処理
        PieceType[] shuffle = pieceTypes.OrderBy(i => Guid.NewGuid()).ToArray();
        count = 0;
        foreach(PieceType pieceType in shuffle){
            deck.Set(count, pieceType);
            count++;
        }
    }

    /// <summary>
    /// カードを1枚ドローする
    /// </summary>
    public void DrawDeck(){
        PieceType result = deck.Get(drawCount);
        drawCount++;
        hand.Add(result);
        Debug.Log(result);
    }
    
    /// <summary>
    /// 手持ちのコマをDebugLogに表示するデバッグ用
    /// </summary>
    public void PrintHand(){
        foreach(PieceType pieceType in hand){
            Debug.Log(pieceType);
        }
    }

}
