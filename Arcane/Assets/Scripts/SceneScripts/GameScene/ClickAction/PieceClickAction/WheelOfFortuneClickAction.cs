using UnityEngine;

public class WheelOfFortuneAction : IClickAction
{
    WheelOfFortune masterPiece;

    public WheelOfFortuneAction(WheelOfFortune masterPiece){
        this.masterPiece = masterPiece;
    }

    PieceObject latestSelectPiece;

    public void OnClickBoard(BoardBlock bb)
    {
    }

    public void OnClickPiece(GameObject pieceObject)
    {
        if(pieceObject.GetComponent<PieceObject>().GetPieceType() == PieceType.WheelOfFortune)return;
        if(!pieceObject.GetComponent<PieceObject>().isMine)return;
        
        latestSelectPiece = pieceObject.GetComponent<PieceObject>();
    }

    public void OnPressButton(){
        if(latestSelectPiece == null) return;
        masterPiece.ExchangePiece(latestSelectPiece);
    }
}