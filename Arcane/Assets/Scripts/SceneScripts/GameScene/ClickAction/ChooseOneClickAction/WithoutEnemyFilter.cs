using UnityEngine;

public class WithoutEnemyFilter : TargetFilter
{
    public override bool filterCondition(GameObject target)
    {
        if(target.GetComponent<PieceObject>() == null) return false;
        if(target.GetComponent<PieceObject>().isMine)return true;
        return false;
    }
}
