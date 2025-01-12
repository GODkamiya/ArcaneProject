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
}
