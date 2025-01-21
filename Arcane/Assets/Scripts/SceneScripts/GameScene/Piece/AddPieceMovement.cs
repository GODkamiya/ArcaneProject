using System.Runtime;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public abstract class AddPieceMovement
{
    public abstract PieceMovement Add(int x, int y, PieceMovement movement);
}
