using UnityEngine;

public class KingSelectAction : IClickAction
{
    private LocalBoardManager localBoardManager;

    public KingSelectAction(LocalBoardManager localBoardManager){
        this.localBoardManager=localBoardManager;
    }
    public void OnClickBoard(BoardBlock bb)
    {
        //null
    }

    public void OnClickPiece(GameObject pieceObject)
    {
        localBoardManager.SelectKing(pieceObject);
    }
}
