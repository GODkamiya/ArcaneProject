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

    // 生きているか
    public NetworkBool isLiving { get; }

    public PieceStateData(bool isKing, bool isReverse, bool isSickness, bool isLiving)
    {
        this.isKing = isKing;
        this.isReverse = isReverse;
        this.isSickness = isSickness;
        this.isLiving = isLiving;
    }

    public PieceStateData CopyWith(bool? isKing = null, bool? isReverse = null, bool? isSickness = null, bool? isLiving = null)
    {
        return new PieceStateData(
            isKing ?? this.isKing,
            isReverse ?? this.isReverse,
            isSickness ?? this.isSickness,
            isLiving ?? this.isLiving
        );
    }
}
