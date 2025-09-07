using UnityEngine;
using VContainer.Unity;

public class PlayerClickHandleController
{

    readonly DescriptionPanelController discriptionPanel;

    public PlayerClickHandleController(
    DescriptionPanelController discriptionPanel)
    {
        this.discriptionPanel = discriptionPanel;
    }

    public void OnClick(GameObject clickedObject)
    {
        if (clickedObject.tag == "Board")
        {
        }
        else if (clickedObject.tag == "Piece")
        {
            RequestShowDescription(clickedObject.GetComponent<PieceObject>());
        }
    }

    /// <summary>
    /// 説明パネルに表示するよう要求する
    /// </summary>
    private void RequestShowDescription(PieceObject piece)
    {
        // もし敵の隠者のコマだったら、表示しない
        if (!piece.isMine && piece is Hermit hermit && hermit.isTransparent) return;

        discriptionPanel.SetPieceInfo(piece);
    }
}
