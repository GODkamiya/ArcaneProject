using System;
using UnityEngine;

public class JusticePhase : IPhase
{
    Justice masterPiece;

    public JusticePhase(Justice masterPiece)
    {
        this.masterPiece = masterPiece;
    }
    public void Enter()
    {
        void lineAction()
        {
            int newX = (BoardManager.BOARD_SIZE - 1) - masterPiece.x;
            var targetPiece = BoardManager.singleton.onlinePieces[newX, masterPiece.y];
            //TODO 移動先に味方の駒がいたら発動できないようにする
            if (targetPiece != null && targetPiece.GetComponent<PieceObject>().isMine) return;
            masterPiece.SetPosition(newX, masterPiece.y, true,false);
            masterPiece.canActive = false;
            GameManager.singleton.phaseMachine.TransitionTo(new ActionPhase());
        }
        void pointAction()
        {
            int newX = (BoardManager.BOARD_SIZE - 1) - masterPiece.x;
            int newY = (BoardManager.BOARD_SIZE - 1) - masterPiece.y;
            var targetPiece = BoardManager.singleton.onlinePieces[newX, newY];
            //TODO 移動先に味方の駒がいたら発動できないようにする
            if (targetPiece != null && targetPiece.GetComponent<PieceObject>().isMine) return;
            masterPiece.SetPosition(newX, newY, true,false);
            masterPiece.canActive = false;
            GameManager.singleton.phaseMachine.TransitionTo(new ActionPhase());
        }
        void cancelAction()
        {
            GameManager.singleton.phaseMachine.TransitionTo(new ActionPhase());
        }
        UIManager.singleton.ShowJusticePanel(lineAction, pointAction, cancelAction, masterPiece.isReverse);

    }

    public void Exit()
    {
        UIManager.singleton.HideJusticePanel();
    }
}
