using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerObject : NetworkBehaviour
{
    // プレイヤーが生成され次第、プレイヤーを登録する
    public override void Spawned(){
        GameManager.singleton.AddPlayerObject_Rpc(GetComponent<NetworkObject>());
    }
}
