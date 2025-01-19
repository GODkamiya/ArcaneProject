using UnityEngine;

public interface IClickAction
{
    public void OnClickPiece(GameObject pieceObject);
    public void OnClickBoard(BoardBlock bb);
}
