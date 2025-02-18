using UnityEngine;

public class RangeFilter : TargetFilter
{
    PieceMovement range;

    public RangeFilter(PieceMovement range){
        this.range = range;
    }
    public override bool filterCondition(GameObject target)
    {
        PieceObject po = target.GetComponent<PieceObject>();
        return range.CanMovement(po.x,po.y);
    }
}
