using Fusion;
using UnityEngine;

/// <summary>
/// コマの特殊状態を管理する
/// </summary>
public struct PieceStateData : INetworkStruct
{

    // コマが王か
    public NetworkBool isKing { get; }

    // 逆位置かどうか
    public NetworkBool isReverse { get; }

    // 召喚酔いしているかどうか
    public NetworkBool isSickness { get; }

    public PieceStateData(bool isKing, bool isReverse, bool isSickness)
    {
        this.isKing = isKing;
        this.isReverse = isReverse;
        this.isSickness = isSickness;
    }

    public PieceStateData CopyWith(bool? isKing = null, bool? isReverse = null, bool? isSickness = null)
    {
        return new PieceStateData(
            isKing ?? this.isKing,
            isReverse ?? this.isReverse,
            isSickness ?? this.isSickness
        );
    }
}
