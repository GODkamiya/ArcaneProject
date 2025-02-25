using UnityEngine;

public class SpecificYFilter : TargetFilter
{
    int y;

    public SpecificYFilter(int y){
        this.y = y;
    }

    public override bool filterCondition(GameObject target)
    {
        return target.GetComponent<PieceObject>().y == y;
    }
}
