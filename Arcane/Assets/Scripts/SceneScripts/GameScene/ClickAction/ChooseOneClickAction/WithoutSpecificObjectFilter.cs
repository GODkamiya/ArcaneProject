using UnityEngine;

public class WithoutSpecificObjectFilter : TargetFilter
{
    GameObject withoutTarget;
    public WithoutSpecificObjectFilter(GameObject withoutTarget){
        this.withoutTarget = withoutTarget;
    }

    public override bool filterCondition(GameObject target)
    {
        return withoutTarget != target;
    }
}
