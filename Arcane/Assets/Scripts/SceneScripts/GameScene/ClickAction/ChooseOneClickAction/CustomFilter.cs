using System;
using UnityEngine;

public class CustomFilter : TargetFilter
{
    private Func<GameObject,bool> condition;
    public CustomFilter(Func<GameObject,bool> condition){
        this.condition = condition;
    }
    public override bool filterCondition(GameObject target)
    {
        return condition(target);
    }
}
