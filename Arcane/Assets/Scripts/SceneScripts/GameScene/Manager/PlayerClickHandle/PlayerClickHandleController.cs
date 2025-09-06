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
            // TODO ここで隠者は見えないようにしなければいけない
            discriptionPanel.SetPieceInfo(clickedObject.GetComponent<PieceObject>());
        }
    }
}
