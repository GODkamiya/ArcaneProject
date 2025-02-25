using UnityEngine;

public class SpecificXFilter : TargetFilter
{
    int x;

    public SpecificXFilter(int x){
        this.x = x;
    }

    public override bool filterCondition(GameObject target)
    {
        return target.GetComponent<PieceObject>().x == x;
    }
}
