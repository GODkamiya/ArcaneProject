using UnityEditor;
using UnityEngine;

public class UDLRAddPieceMovement : AddPieceMovement
{
    int addValue;

    public UDLRAddPieceMovement(int addValue){
        this.addValue = addValue;
    }

    public override PieceMovement Add(int x, int y, PieceMovement movement)
    {
        // TODO 汚すぎるのでリファクタ対象。
        // 方向を{0,1}{0,-1}みたいにもって、それを書けると一個のforで対処できそう
        PieceMovement newMovement = movement.Copy();
        for(int i  = 1 ; i < 10; i++){
            if(!newMovement.CanMovement(x+i,y)) {
                newMovement.AddRange(x+i,y);
                break;
            }
        }
        for(int i  = 1 ; i < 10; i++){
            if(!newMovement.CanMovement(x-i,y)) {
                newMovement.AddRange(x-i,y);
                break;
            }
        }
        for(int i  = 1 ; i < 10; i++){
            if(!newMovement.CanMovement(x,y+i)) {
                newMovement.AddRange(x,y+i);
                break;
            }
        }
        for(int i  = 1 ; i < 10; i++){
            if(!newMovement.CanMovement(x,y-i)) {
                newMovement.AddRange(x,y - i);
                break;
            }
        }
        return newMovement;
    }
}
