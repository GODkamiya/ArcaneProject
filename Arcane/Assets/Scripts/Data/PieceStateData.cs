using Fusion;
using UnityEngine;

/// <summary>
/// コマの特殊状態を管理する
/// </summary>
public struct PieceStateData
{

    // コマが王か
    public bool isKing { get; }

    // 逆位置かどうか
    public bool isReverse { get; }

    // 召喚酔いしているかどうか
    public bool isSickness { get; }

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

    /// <summary>
    /// コマが技を発動可能かどうか
    /// </summary>
    public bool CanSpell()
    {
        return !isSickness;
    }

    /// <summary>
    /// コマが移動可能かどうか
    /// </summary>
    public bool CanMove()
    {
        return !isSickness;
    }
}
