using UnityEngine;

public class ReverseFilter : TargetFilter
{
    public override bool filterCondition(GameObject target)
    {
        return target.GetComponent<PieceObject>().isReverse;
    }
}
