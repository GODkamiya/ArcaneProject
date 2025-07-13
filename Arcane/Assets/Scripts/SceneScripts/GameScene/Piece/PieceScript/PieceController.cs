using UnityEngine;

public class PieceController
{
    // コマの特殊状態
    private PieceStateData stateData;

    public PieceController()
    {
        stateData = new PieceStateData(isKing: false, isReverse: false, isSickness: true);
    }

    /// <summary>
    /// コマが技発動可能か
    /// </summary>
    public bool GetCanSpell => stateData.CanSpell();

    /// <summary>
    /// コマが移動可能か
    /// </summary>
    public bool GetCanMove => stateData.CanMove();

    /// <summary>
    /// コマが王かどうか
    /// </summary>
    public bool GetIsKing => stateData.isKing;

    /// <summary>
    /// コマが王かどうか設定する
    /// </summary>
    public void SetKing(bool isKing) => stateData = stateData.CopyWith(isKing: isKing);

    /// <summary>
    /// 召喚酔いの状態を更新する
    /// </summary>
    public void SetSickness(bool isSickness) => stateData = stateData.CopyWith(isSickness: isSickness);

    /// <summary>
    /// 逆位置の状態を更新する
    /// </summary>
    public void SetReverse(bool isReverse) => stateData = stateData.CopyWith(isReverse: isReverse);

}
