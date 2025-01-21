using System;
using UnityEngine;

public class TurnEndEvent
{
    int lingerTurn;
    Action turnEndAction;

    public TurnEndEvent(int lingerTurn,Action turnEndAction)
    {
        this.lingerTurn = lingerTurn;
        this.turnEndAction = turnEndAction;
    }

    public void Do(){
        lingerTurn--;
        if(lingerTurn <= 0)turnEndAction();
    }
}
