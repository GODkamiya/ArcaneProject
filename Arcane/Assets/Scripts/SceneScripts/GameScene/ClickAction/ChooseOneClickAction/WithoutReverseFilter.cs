using UnityEngine;

public class WithoutReverseFilter : TargetFilter
{
    public override bool filterCondition(GameObject target)
    {
        return !target.GetComponent<PieceObject>().isReverse;
    }
}
