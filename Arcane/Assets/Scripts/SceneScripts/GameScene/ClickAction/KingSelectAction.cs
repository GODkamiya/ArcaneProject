using UnityEngine;

public class KingSelectAction : IClickAction
{
    public void OnClickBoard(BoardBlock bb)
    {
        //null
    }

    public void OnClickPiece(GameObject pieceObject)
    {
        GameManager.singleton.GetLocalPlayerObject().SetKingPieceType(pieceObject);
        
    }
}
