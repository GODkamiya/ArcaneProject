using System;

/// <summary>
/// コマの説明パネルを制御するクラス
/// </summary>
public class DescriptionPanelController
{

    // 現在表示しているのが逆位置かどうか
    private bool isReverse = false;

    // 現在表示しているコマ
    private PieceObject currentPieceObject;

    /// <summary>
    /// 選択中のコマが切り替わったときのイベント
    /// </summary>
    public event Action<string/*コマの名前*/, string/*コマのアビリティ説明*/, bool/*コマが逆位置かどうか*/, int[,]/*移動範囲*/> OnSetPieceInfo;

    /// <summary>
    /// 選択中のコマが切り替わったとき
    /// </summary>
    public void SetPieceInfo(PieceObject pieceObject)
    {
        currentPieceObject = pieceObject;
        // コマの情報を[OnSetPieceInfo]イベントに流す
        string name = pieceObject.GetName();
        string description = isReverse ? pieceObject.GetReverseEffectDescription() : pieceObject.GetUprightEffectDescription();
        int[,] movementDefinitions = !isReverse ? pieceObject.GetMovementDefinitions() : pieceObject.GetReverseMovementDefinitions() ?? pieceObject.GetMovementDefinitions();
        OnSetPieceInfo?.Invoke(name, description, isReverse, movementDefinitions);
    }

    /// <summary>
    /// 対象のコマの正位置の説明を取得する
    /// </summary>
    public string GetPieceUprightDescription() => currentPieceObject.GetUprightEffectDescription();

    public string GetPieceReverseDescription() => currentPieceObject.GetReverseEffectDescription();

    public int[,] GetPieceMovementDefinitions() => currentPieceObject.GetMovementDefinitions();

    public int[,] GetPieceReverseMovementDefinitions() => currentPieceObject.GetReverseMovementDefinitions();
}
