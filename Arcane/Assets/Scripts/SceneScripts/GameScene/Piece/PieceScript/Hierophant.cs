using UnityEngine;

public class Hierophant : ActivePieceObject
{
    public override void ActiveEffect()
    {
        GameManager.singleton.phaseMachine.TransitionTo(new HierophantPhase(this));
    }

    public override string GetName()
    {
        return "Hierophant";
    }

    public override PieceMovement GetPieceMovementOrigin(int baseX,int baseY)
    {
        PieceMovement pm = new PieceMovement();
        for (int addX = -1; addX < 2; addX++)
        {
            for (int addY = -1; addY < 2; addY++)
            {
                if(addX == 0 && addY == 0)continue;
                pm.AddRange(baseX + addX, baseY + addY);
            }
        }
        return pm;
    }

    public override PieceType GetPieceType()
    {
        return PieceType.Hierophant;
    }

    public void AddMovement(GameObject target){
        canActive = false;
        var buff = new UDLRAddPieceMovement(1);
        target.GetComponent<PieceObject>().AddAddPieceMovement(buff);
        GameManager.singleton.turnEndEvents.Add(
            new TurnEndEvent(1,()=>target.GetComponent<PieceObject>().RemoveAddPieceMovement(buff))
        );
        GameManager.singleton.phaseMachine.TransitionTo(new ActionPhase());
    }
}
