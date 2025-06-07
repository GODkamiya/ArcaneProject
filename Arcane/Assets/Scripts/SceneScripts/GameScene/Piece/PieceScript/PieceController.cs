using UnityEngine;

public class PieceController
{
    // コマの特殊状態
    private PieceStateData stateData;

    public PieceController(PieceObject pieceObject)
    {
        stateData = new PieceStateData(isKing: false, isReverse: false, isSickness: true);
    }

    // コマが技発動可能か
    public bool GetCanSpell => stateData.CanSpell();

    // コマが移動可能か
    public bool GetCanMove => stateData.CanMove();

    // 召喚酔いの状態を更新する
    public void SetSickness(bool isSickness) => stateData = stateData.CopyWith(isSickness: isSickness);
}
