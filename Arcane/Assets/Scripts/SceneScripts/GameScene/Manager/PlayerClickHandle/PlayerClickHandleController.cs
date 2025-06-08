using UnityEngine;
using VContainer.Unity;

public class PlayerClickHandleController
{

    readonly DescriptionPanelController discriptionPanel;

    public PlayerClickHandleController(PlayerClickHandleManager playerClickHandleManager,
    DescriptionPanelController discriptionPanel)
    {
        this.discriptionPanel = discriptionPanel;
        playerClickHandleManager.onClick += OnClick;
    }

    void OnClick(GameObject clickedObject)
    {
        Debug.Log("TestTestTest");
        if (clickedObject.tag == "Board")
        {
        }
        else if (clickedObject.tag == "Piece")
        {
            discriptionPanel.SetPieceInfo(clickedObject.GetComponent<PieceObject>());
        }
    }
}
