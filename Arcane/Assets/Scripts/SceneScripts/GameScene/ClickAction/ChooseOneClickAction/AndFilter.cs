using UnityEngine;

public class AndFilter : TargetFilter
{
    TargetFilter[] filters;

    public AndFilter(params TargetFilter[] filters){
        this.filters = filters;
    }
    public override bool filterCondition(GameObject target)
    {
        foreach(var filter in filters){
            if(!filter.filterCondition(target))return false;
        }
        return true;
    }
}
