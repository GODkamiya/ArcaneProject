using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// コマの説明パネルを制御するクラス
/// </summary>
public class DescriptionPanelController
{
    readonly DescriptionPanelViewObject viewObject;

    // 現在表示しているのが逆位置かどうか
    private bool isReverse = false;

    // 現在表示しているコマ
    private PieceObject currentPieceObject;

    public DescriptionPanelController(DescriptionPanelViewObject descriptionPanelViewObject)
    {
        viewObject = descriptionPanelViewObject;
        viewObject.standardButton.GetComponent<Button>().onClick.AddListener(OnClickStandardButton);
        viewObject.reverseButton.GetComponent<Button>().onClick.AddListener(OnClickReverseButton);
    }

    public void SetPieceInfo(PieceObject pieceObject)
    {
        currentPieceObject = pieceObject;
        viewObject.ShowPieceName(pieceObject.GetName());
        viewObject.ShowPieceDescription(isReverse ? pieceObject.GetReverseEffectDescription() : pieceObject.GetUprightEffectDescription(), isReverse);
    }

    private void OnClickStandardButton()
    {
        isReverse = false;
        viewObject.ShowPieceDescription(currentPieceObject.GetUprightEffectDescription(), isReverse);
    }

    private void OnClickReverseButton()
    {
        isReverse = true;
        viewObject.ShowPieceDescription(currentPieceObject.GetReverseEffectDescription(), isReverse);
    }
}
