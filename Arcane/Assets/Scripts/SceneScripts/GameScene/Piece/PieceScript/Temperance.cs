using Fusion;
using UnityEngine;

public class Temperance : ActivePieceObject, IOnReverse, IOnAfterDeath
{
    public GameObject target;

    public int counter = 1;

    public override void ActiveEffect()
    {
        GameManager.singleton.phaseMachine.TransitionTo(new TemperancePhase(this));
    }

    [Rpc(RpcSources.StateAuthority,RpcTargets.All)]
    public void Effect_RPC(NetworkObject target){
        counter--;
        this.target = target.gameObject;
        target.GetComponent<PieceObject>().SetTemperance(gameObject);
    }

    public override bool CanSpellActiveEffect()
    {
        if(counter <= 0) return false;
        return canActive;
    }

    public override string GetName()
    {
        return "節制";
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
        return PieceType.Temperance;
    }

    public void OnReverse()
    {
        counter++;
    }

    public void OnAfterDeath(int x, int y)
    {
        target.GetComponent<PieceObject>().SetTemperance(null);
    }

    public override string GetUprightEffectDescription()
    {
        return "カウンターを1つ消費して、指定したコマ１体とこのコマの動きや効果を封じる。どちらかのコマが倒れたとき、再び動くこと・効果を使用することが可能になる。";
    }

    public override string GetReverseEffectDescription()
    {
        return "カウンターを1つ増やす。";
    }
}
