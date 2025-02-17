using UnityEngine;

public class WithoutAllyFilter : TargetFilter
{
    public override bool filterCondition(GameObject target)
    {
        return !target.GetComponent<PieceObject>().isMine;
    }
}
