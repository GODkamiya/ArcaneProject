using System;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;

public class PieceMovement
{
    public bool[,] range = new bool[10, 10];
    public void AddRange(int x, int y)
    {
        if (x < 0) return;
        if (y < 0) return;
        if (x > 9) return;
        if (y > 9) return;
        range[x, y] = true;
    }

    public PieceMovement Copy(){
        PieceMovement pm = new PieceMovement();
        for(int i = 0 ; i < 10 ; i++){
            for(int j = 0 ; j < 10; j++){
                if(range[i,j])pm.AddRange(i,j);
            }
        }
        return pm;
    }

    public bool CanMovement(int x, int y){
        if (x < 0) return false;
        if (y < 0) return false;
        if (x > 9) return false;
        if (y > 9) return false;
        return range[x,y];
    }
}
