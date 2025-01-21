using UnityEngine;

public class Justice : ActivePieceObject
{
    const int BOARD_SIZE = 10;
    public override void ActiveEffect()
    {
        canActive = false;
        int newX = (BOARD_SIZE - 1) - x;
        var targetPiece = BoardManager.singleton.onlinePieces[newX, y];
        //TODO 移動先に味方の駒がいたら発動できないようにする
        if (targetPiece != null && targetPiece.GetComponent<PieceObject>().isMine) return;
        SetPosition(newX, y, true);
        GameManager.singleton.phaseMachine.TransitionTo(new ActionPhase());
    }

    public override string GetName()
    {
        return "Justice";
    }

    public override PieceMovement GetPieceMovement()
    {
        PieceMovement pm = new PieceMovement();
        for (int addX = -1; addX < 2; addX++)
        {
            for (int addY = -1; addY < 2; addY++)
            {
                if (addX == 0 && addY == 0) continue;
                pm.AddRange(x + addX, y + addY);
            }
        }
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Justice;
    }
}
