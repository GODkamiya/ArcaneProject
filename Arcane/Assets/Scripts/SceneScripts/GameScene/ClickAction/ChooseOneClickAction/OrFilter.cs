using UnityEngine;

public class OrFilter : TargetFilter
{
    TargetFilter[] filters;

    public OrFilter(params TargetFilter[] filters){
        this.filters = filters;
    }
    public override bool filterCondition(GameObject target)
    {
        foreach(var filter in filters){
            if(filter.filterCondition(target))return true;
        }
        return false;
    }
}
